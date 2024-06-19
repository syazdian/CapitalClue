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

namespace StockModelBuilderConsole
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            //1- Read list of CSV files
            //Read CSV Files:

            string csv2019Folder = @"C:\\CapitalClueStockData\csv\2019"; // Replace with your folder path

            List<string> csvFiles = new List<string>(Directory.EnumerateFiles(csv2019Folder, "*.csv", SearchOption.AllDirectories));

            //2- Read each CSV file
            for (int counterRun = 1; counterRun < 11; counterRun += 1)
            {
                Console.WriteLine($"----------Create Model:{counterRun}----------");
                string modelCreatedFolderName = $"Stock_trainsize128_{counterRun}";

                foreach (string file in csvFiles)
                {
                    StockModelDto stockModelDto = new StockModelDto();

                    string fileName = Path.GetFileNameWithoutExtension(file);
                    Console.WriteLine($"Reading file: {fileName}");
                    // Read the contents of the file
                    string csvValue = File.ReadAllText(file);
                    if (csvValue is null) return;
                    try
                    {
                        stockModelDto.StockValueIndices = await ReadCSVFile(csvValue);
                        var stockProps = fileName.Replace(".csv", "").Split('-');

                        stockModelDto.StockName = stockProps[0];
                        stockModelDto.Currency = stockProps[1];

                        //3-Send to Model Builder

                        await BuildPropertyModel(stockModelDto, counterRun, modelCreatedFolderName);
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync($"ReadMapUpload: {ex.Message}");
                        throw;
                    }
                }
                Console.WriteLine($"----------Evaluate:{counterRun}----------");

                await Evaluate(modelCreatedFolderName);
                Console.WriteLine($"----------Done:{counterRun}----------");
            }

            //3- EVALUATE
            //Read Model Files:

            //2- Read each CSV file
        }

        public static async Task<List<StockValueIndex>> ReadCSVFile(string csvFile)
        {
            try
            {
                List<string> errorMessages;

                List<StockValueIndex> stockValueIndices = new();
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
                if (Array.IndexOf(columnNames, "open") == -1) errorMessages.Add("column name open not found");

                int lineNumnber = 0;
                foreach (var line in lines)
                {
                    if (line.Length < 2) continue;

                    errorMessages = new List<string>();

                    StockValueIndex stockvalueIndex = new();
                    string[] values = line.Split(',');
                    if (values.Count() == 2)
                    {
                        values[1] = values[1].TrimEnd('\r');
                        values = values.Select(s => s.Trim()).ToArray();
                        if (!DateTime.TryParse(values[Array.IndexOf(columnNames, "date")].Trim(), out DateTime transactionDate))
                        {
                            errorMessages.Add("TRANSACTION DATE is invalid");
                        }
                        else
                        {
                            stockvalueIndex.DateTime = transactionDate;
                        }
                    }
                    else
                    {
                        stockvalueIndex.DateTime = new DateTime(2019, 04, 14);
                    }

                    if (float.TryParse(values[Array.IndexOf(columnNames, "open")].Trim(), out float price))
                    {
                        stockvalueIndex.Value = price;
                    }

                    stockValueIndices.Add(stockvalueIndex);
                    lineNumnber++;
                    await Task.Delay(1);
                }

                return stockValueIndices;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"ReadCSVFile: {ex.Message}");
                throw;
            }
        }

        public static async Task BuildPropertyModel(StockModelDto stock, int counterRun, string modelFolderName)
        {
            string ExportModelFileName = "PropertyModel.zip";
            MLContext context;
            IDataView data;

            context = new MLContext();
            data = context.Data.LoadFromEnumerable<StockValueIndex>(stock.StockValueIndices);
            ExportModelFileName = string.Format("{0}-{1}-{2}", stock.StockName, stock.Currency, ExportModelFileName);
            ForecastingCatalog forecastingCatalog;// = new() { Trainers = property.PropertyValueIndices };
            var pipline = context.Forecasting.ForecastBySsa(

             outputColumnName: nameof(PropertyPredictionEntity.ForeCastIndex),
             inputColumnName: nameof(PropertyValueIndex.Value),
             confidenceLevel: 0.95F,
             confidenceLowerBoundColumn: nameof(PropertyPredictionEntity.ConfidenceLowerBound),
             confidenceUpperBoundColumn: nameof(PropertyPredictionEntity.ConfidenceUpperBound),

              windowSize: 250,
               seriesLength: 5059,
               trainSize: 6500,
                 horizon: 1258,
             rankSelectionMethod: RankSelectionMethod.Fixed,
              rank: 93,
               // rankSelectionMethod: RankSelectionMethod.Exact,
               // //rankSelectionMethod: RankSelectionMethod.Fast,

               //   maxRank: 28
               //  horizon: 29,

               //  isAdaptive: true,
               discountFactor: 0.5f// (counterRun * 0.1f)
                                   //shouldStabilize: true,
                                   //shouldMaintainInfo: true,
                                   // maxGrowth: new GrowthRatio() { Growth = 1.2, TimeSpan = 29 }

             // Example value
             //  maxGrowth: new Microsoft.ML.Transforms.TimeSeries.GrowthRatio?() {CancellationTokenRegistration     Ratio = 1.1}

             );

            var model = pipline.Fit(data);

            string ExportDirectory = $"C:\\CapitalClueStockData\\TrainedModels\\{modelFolderName}";
            if (!Directory.Exists(ExportDirectory)) Directory.CreateDirectory(ExportDirectory);

            context.Model.Save(model, data.Schema, $"{ExportDirectory}/{ExportModelFileName}");

            await Console.Out.WriteLineAsync($"successfully build Property Model{stock.StockName}- {stock.Currency}");
        }

        public static async Task Evaluate(string modelFolderName)
        {
            string csv2024Folder = @"C:\CapitalClueStockData\csv";
            string summaryCompareTxt = @"C:\CapitalClueStockData\csv\Compare\totalCompare.txt";
            string summaryCompareCSV = @"C:\CapitalClueStockData\csv\Compare\summaryCompareCSV.csv";
            string readModelFolderPath = $"C:\\CapitalClueStockData\\TrainedModels\\{modelFolderName}"; // Replace with your folder path

            List<string> zipFiles = new List<string>(Directory.EnumerateFiles(readModelFolderPath, "*.zip", SearchOption.AllDirectories));

            List<float> ErrorMainList = new List<float>();
            List<float> ErrorLowerList = new List<float>();
            List<float> ErrorHigherList = new List<float>();

            string csvCompareResultOutput = $"C:\\CapitalClueStockData\\csv\\Compare\\{modelFolderName}.csv";
            if (!File.Exists(csvCompareResultOutput)) File.Create(csvCompareResultOutput).Close();
            File.AppendAllText(csvCompareResultOutput, $"StockName, Real Number, ForeCastIndex,Error,ErrorPercent, LowerBound, ErrorLower,LowerPercent, HigherBound,ErrorHigher, HigherPercent{Environment.NewLine}");

            string Stock; string Currency;
            foreach (string file in zipFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                Console.WriteLine($"ModelReading file: {fileName}");
                var stockProps = fileName.Replace(".zip", "").Split('-');
                Stock = stockProps[0];
                Currency = stockProps[1];

                //3-Send to Predictor
                var predictedRes = GetPredictionYearByYear(Stock, Currency, readModelFolderPath);

                //4- Compare with current CSV
                //Read CSV
                var lastLine = File.ReadLines($"{csv2024Folder}/{Stock}-{Currency}.csv").Last().Split(',');
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

                string toPrintLine = $"{Stock}-{Currency}," +
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

            File.AppendAllText(summaryCompareTxt, $"{Environment.NewLine}{Path.GetFileNameWithoutExtension(csvCompareResultOutput)}{Environment.NewLine}");

            File.AppendAllText(summaryCompareTxt, $"MAE_main:{MAE_main.ToString("0.00")}{Environment.NewLine}");
            File.AppendAllText(summaryCompareTxt, $"RMSE_main:{RMSE_main.ToString("0.00")}{Environment.NewLine}");
            File.AppendAllText(summaryCompareTxt, $"MAE_lower:{MAE_lower.ToString("0.00")} {Environment.NewLine}");
            File.AppendAllText(summaryCompareTxt, $"RMSE_lower:{RMSE_lower.ToString("0.00")}{Environment.NewLine}");
            File.AppendAllText(summaryCompareTxt, $"MAE_higher:{MAE_higher.ToString("0.00")} {Environment.NewLine}");
            File.AppendAllText(summaryCompareTxt, $"RMSE_higher:{RMSE_higher.ToString("0.00")}{Environment.NewLine}");
            File.AppendAllText(summaryCompareTxt, $"-------------------------------");

            File.AppendAllText(summaryCompareCSV, $"{modelFolderName}, {MAE_main.ToString("0.00")}, {RMSE_main.ToString("0.00")}, {MAE_lower.ToString("0.00")}, {RMSE_lower.ToString("0.00")},  {MAE_higher.ToString("0.00")},{RMSE_higher.ToString("0.00")} {Environment.NewLine}");
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