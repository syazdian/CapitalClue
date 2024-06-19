using CapitalClue.Common.Models;
using CapitalClue.Common.Utilities;
using CapitalClue.Web.Server.ML.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace PropertyModelBuilderConsole
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            //1- Read list of CSV files
            //Read CSV Files:

            string csv2019Folder = @"C:\CapitalClueData\csv"; // Replace with your folder path

            List<string> csvFiles = new List<string>(Directory.EnumerateFiles(csv2019Folder, "*.csv", SearchOption.AllDirectories));

            //2- Read each CSV file
            for (int counterRun = 1; counterRun < 2; counterRun += 1)
            {
                Console.WriteLine($"----------Create Model:{counterRun * 0.1}----------");
                string modelCreatedFolderName = $"Property_2024_{counterRun}";

                foreach (string file in csvFiles)
                {
                    PropertyModelDto propertyModelDto = new PropertyModelDto();

                    string fileName = Path.GetFileNameWithoutExtension(file);
                    Console.WriteLine($"Reading file: {fileName}");
                    // Read the contents of the file
                    string csvValue = File.ReadAllText(file);
                    if (csvValue is null) return;
                    try
                    {
                        propertyModelDto.PropertyValueIndices = await ReadCSVFile(csvValue);
                        var cityPropery = fileName.Replace(".csv", "").Split('-');

                        propertyModelDto.City = cityPropery[0];
                        propertyModelDto.PropertyType = cityPropery[1];

                        //3-Send to Model Builder

                        await BuildPropertyModel(propertyModelDto, counterRun, modelCreatedFolderName);
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync($"ReadMapUpload: {ex.Message}");
                        throw;
                    }
                }
                Console.WriteLine($"----------Evaluate:{counterRun * 0.1}----------");

                //await Evaluate(modelCreatedFolderName);
                //Console.WriteLine($"----------Done:{counterRun * 0.1}----------");
            }

            //3- EVALUATE
            //Read Model Files:

            //2- Read each CSV file
        }

        public static async Task<List<PropertyValueIndex>> ReadCSVFile(string csvFile)
        {
            try
            {
                List<string> errorMessages;

                List<PropertyValueIndex> propertyValueIndices = new();
                var lines = csvFile.Split('\n').ToList();

                //if a row has less than a character length, that row is not valid and should be removed
                if (lines[lines.Count - 1].Length < 2)
                {
                    lines.RemoveAt(lines.Count - 1);
                }

                string firstLine = lines[0].TrimEnd('\r');
                string[] columnNames = firstLine.ToLower().Split(',');
                columnNames = columnNames.Select(s => s.Trim()).Select(s => s.Replace("\uFEFF", "")).ToArray();
                lines.RemoveAt(0);

                errorMessages = new List<string>();
                if (Array.IndexOf(columnNames, "date") == -1) errorMessages.Add("column name DATE not found");
                if (Array.IndexOf(columnNames, "price") == -1) errorMessages.Add("column name Price not found");

                int lineNumnber = 0;
                foreach (var line in lines)
                {
                    errorMessages = new List<string>();

                    PropertyValueIndex propertyValueIndex = new();
                    string[] values = line.Split(',');

                    values[columnNames.Length - 1] = values[columnNames.Length - 1].TrimEnd('\r');
                    values = values.Select(s => s.Trim()).ToArray();

                    if (!DateTime.TryParse(values[Array.IndexOf(columnNames, "date")].Trim(), out DateTime transactionDate))
                    {
                        errorMessages.Add("TRANSACTION DATE is invalid");
                    }
                    else
                    {
                        propertyValueIndex.DateTime = transactionDate;
                    }

                    if (float.TryParse(values[Array.IndexOf(columnNames, "price")].Trim(), out float price))
                    {
                        propertyValueIndex.Value = price;
                    }

                    propertyValueIndices.Add(propertyValueIndex);
                    lineNumnber++;
                    await Task.Delay(1);

                    // if (errorCounter == 10)
                    // {
                    //     st2AlertStyleAskToUpload = AlertStyle.Danger;
                    //     St2MessageInAlert = $"Your Csv File has error(s). Please fix them and try again.";
                    //     break;
                    // }
                }

                // _stateContainer.StoreNumberDisplay = stores;

                return propertyValueIndices;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"ReadCSVFile: {ex.Message}");
                throw;
            }
        }

        public static async Task BuildPropertyModel(PropertyModelDto property, int counterRun, string modelFolderName)
        {
            string ExportModelFileName = "PropertyModel.zip";
            MLContext context;
            IDataView data;

            context = new MLContext();
            data = context.Data.LoadFromEnumerable<PropertyValueIndex>(property.PropertyValueIndices);
            ExportModelFileName = string.Format("{0}-{1}-{2}", property.City, property.PropertyType, ExportModelFileName);
            ForecastingCatalog forecastingCatalog;// = new() { Trainers = property.PropertyValueIndices };
            var pipline = context.Forecasting.ForecastBySsa(

             outputColumnName: nameof(PropertyPredictionEntity.ForeCastIndex),
             inputColumnName: nameof(PropertyValueIndex.Value),
             confidenceLevel: 0.95F,
             confidenceLowerBoundColumn: nameof(PropertyPredictionEntity.ConfidenceLowerBound),
             confidenceUpperBoundColumn: nameof(PropertyPredictionEntity.ConfidenceUpperBound),
              windowSize: 29,
               seriesLength: 673,
               trainSize: 673,
                 horizon: 112,
                 //  horizon: 29,

                 //  isAdaptive: true,
                 // discountFactor: (counterRun * 0.1f),
                 rankSelectionMethod: RankSelectionMethod.Fixed,
                   // // rankSelectionMethod: RankSelectionMethod.Exact,
                   // //rankSelectionMethod: RankSelectionMethod.Fast,
                   rank: 1,

                  maxRank: 28
             //shouldStabilize: true,
             //shouldMaintainInfo: true,
             //  maxGrowth: new Microsoft.ML.Transforms.TimeSeries.GrowthRatio?() {CancellationTokenRegistration     Ratio = 1.1}
             //  maxGrowth: new GrowthRatio() { Growth = 1.2, TimeSpan = 29 } // Example value

             );

            var model = pipline.Fit(data);

            string ExportDirectory = $"C:\\CapitalClueData\\TrainedModels\\{modelFolderName}";
            if (!Directory.Exists(ExportDirectory)) Directory.CreateDirectory(ExportDirectory);

            context.Model.Save(model, data.Schema, $"{ExportDirectory}/{ExportModelFileName}");

            await Console.Out.WriteLineAsync($"successfully build Property Model{property.City}- {property.PropertyType}");
        }

        public static async Task Evaluate(string modelFolderName)
        {
            string csv2024Folder = @"C:\CapitalClueData\csv";
            string summaryCompareTxt = @"C:\CapitalClueData\csv\Compare\totalCompare.txt";
            string summaryCompareCSV = @"C:\CapitalClueData\csv\Compare\summaryCompareCSV.csv";
            string readModelFolderPath = $"C:\\CapitalClueData\\TrainedModels\\{modelFolderName}"; // Replace with your folder path

            List<string> zipFiles = new List<string>(Directory.EnumerateFiles(readModelFolderPath, "*.zip", SearchOption.AllDirectories));

            List<float> ErrorMainList = new List<float>();
            List<float> ErrorLowerList = new List<float>();
            List<float> ErrorHigherList = new List<float>();

            string csvCompareResultOutput = $"C:\\CapitalClueData\\csv\\Compare\\{modelFolderName}.csv";
            if (!File.Exists(csvCompareResultOutput)) File.Create(csvCompareResultOutput).Close();
            File.AppendAllText(csvCompareResultOutput, $"CityProperty, Real Number, ForeCastIndex,Error,ErrorPercent, LowerBound, ErrorLower,LowerPercent, HigherBound,ErrorHigher, HigherPercent{Environment.NewLine}");

            string City; string PropertyType;
            foreach (string file in zipFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                Console.WriteLine($"ModelReading file: {fileName}");
                var cityPropery = fileName.Replace(".zip", "").Split('-');
                City = cityPropery[0];
                PropertyType = cityPropery[1];

                //3-Send to Predictor
                var predictedRes = GetPredictionYearByYear(City, PropertyType, readModelFolderPath);

                //4- Compare with current CSV
                //Read CSV
                var lastLine = File.ReadLines($"{csv2024Folder}/{City}-{PropertyType}.csv").Last().Split(',');
                float csvresult = float.Parse(lastLine[1]);
                float ErrorMain = Math.Abs(predictedRes.ForeCastIndex.Last() - csvresult);
                var ErrorMainPercent = (ErrorMain / csvresult * 100);
                ErrorMainList.Add(ErrorMainPercent);
                float ErrorLower = Math.Abs(predictedRes.ConfidenceLowerBound.Last() - csvresult);
                var ErrorLowerPercent = (ErrorLower / csvresult * 100);
                ErrorLowerList.Add(ErrorLowerPercent);
                float ErrorHigher = Math.Abs(predictedRes.ConfidenceUpperBound.Last() - csvresult);
                var ErrorHigherPercent = (ErrorHigher / csvresult * 100);
                ErrorHigherList.Add(ErrorHigherPercent);

                string toPrintLine = $"{City}-{PropertyType}," +
                    $"{csvresult}," +
                    $"{predictedRes.ForeCastIndex.Last().ToString("0.00")}, {ErrorMain.ToString("0.00")}, {ErrorMainPercent.ToString("0.00")}," +
                    $"{predictedRes.ConfidenceLowerBound.Last().ToString("0.00")},{ErrorLower.ToString("0.00")},{ErrorLowerPercent.ToString("0.00")}," +
                    $"{predictedRes.ConfidenceUpperBound.Last().ToString("0.00")},{ErrorHigher.ToString("0.00")},{ErrorHigherPercent.ToString("0.00")}";

                File.AppendAllText(csvCompareResultOutput, $"{toPrintLine}{Environment.NewLine}");
            }

            float MAE_main = ErrorMainList.Average(); // Mean Absolute Error
            double RMSE_main = Math.Sqrt(ErrorMainList.Average(error => Math.Pow(error, 2))); // Root Mean Squared Error

            float MAE_lower = ErrorLowerList.Average(); // Mean Absolute Error
            double RMSE_lower = Math.Sqrt(ErrorLowerList.Average(error => Math.Pow(error, 2))); // Root Mean Squared Error

            float MAE_higher = ErrorHigherList.Average(); // Mean Absolute Error
            double RMSE_higher = Math.Sqrt(ErrorHigherList.Average(error => Math.Pow(error, 2))); // Root Mean Squared Error

            File.AppendAllText(summaryCompareCSV, $"{modelFolderName}, {MAE_main.ToString("0.00")}, {RMSE_main.ToString("0.00")}, {MAE_lower.ToString("0.00")}, {RMSE_lower.ToString("0.00")},  {MAE_higher.ToString("0.00")},{RMSE_higher.ToString("0.00")} {Environment.NewLine}");
            File.AppendAllText(summaryCompareTxt, $"{Environment.NewLine}{Path.GetFileNameWithoutExtension(csvCompareResultOutput)}{Environment.NewLine}");

            File.AppendAllText(summaryCompareTxt, $"MAE_main:{MAE_main.ToString("0.00")}{Environment.NewLine}");
            File.AppendAllText(summaryCompareTxt, $"RMSE_main:{RMSE_main.ToString("0.00")}{Environment.NewLine}");
            File.AppendAllText(summaryCompareTxt, $"MAE_lower:{MAE_lower.ToString("0.00")} {Environment.NewLine}");
            File.AppendAllText(summaryCompareTxt, $"RMSE_lower:{RMSE_lower.ToString("0.00")}{Environment.NewLine}");
            File.AppendAllText(summaryCompareTxt, $"MAE_higher:{MAE_higher.ToString("0.00")} {Environment.NewLine}");
            File.AppendAllText(summaryCompareTxt, $"RMSE_higher:{RMSE_higher.ToString("0.00")}{Environment.NewLine}");
            File.AppendAllText(summaryCompareTxt, $"-------------------------------");

            ErrorMainList.Clear();
            ErrorLowerList.Clear();
            ErrorHigherList.Clear();
        }

        public static PropertyPredictionEntity GetPredictionYearByYear(string City, string PropertyType, string readFolderPath)
        {
            MLContext context;
            context = new MLContext();
            DataViewSchema modelSchema;
            ITransformer trainedModel;

            string ModelFileName = "PropertyModel.zip";

            ModelFileName = string.Format("{0}-{1}-{2}", City, PropertyType, ModelFileName);
            //var binDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            trainedModel = context.Model.Load($"{readFolderPath}/{ModelFileName}", out modelSchema);

            var ForeCastEngein = trainedModel.CreateTimeSeriesEngine<PropertyValueIndex, PropertyPredictionEntity>(context);
            PropertyPredictionEntity result = ForeCastEngein.Predict();
            return result;
        }
    }
}