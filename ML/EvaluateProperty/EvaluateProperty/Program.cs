using CapitalClue.Common.Models;
using CapitalClue.Web.Server.ML.Entities;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using System.IO;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EvaluateProperty
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string readModelFolderPath = @"C:/TrainedModels/IsAdaptiveTrue"; // Replace with your folder path
            string readCSVSourcePath = @"C:/csv"; // Replace with your folder path
            string compareResult = @"C:\csv\Compare\IsAdaptiveTrue.csv";
            string totalCompareResultsTxt = @"C:\csv\Compare\totalCompare.txt";
            //Create a loop to read city + property Type
            //Read Model Files:
            List<string> zipFiles = new List<string>(Directory.EnumerateFiles(readModelFolderPath, "*.zip", SearchOption.AllDirectories));

            string City; string PropertyType;
            //2- Read each CSV file

            List<float> ErrorMainList = new List<float>();
            List<float> ErrorLowerList = new List<float>();
            List<float> ErrorHigherList = new List<float>();
            using (StreamWriter writer = new StreamWriter(compareResult))
            {
                writer.WriteLine($"CityProperty, Real Number, ForeCastIndex,Error,ErrorPercent, LowerBound, ErrorLower,LowerPercent, HigherBound,ErrorHigher, HigherPercent");
                foreach (string file in zipFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    Console.WriteLine($"Reading file: {fileName}");
                    var cityPropery = fileName.Replace(".zip", "").Split('-');
                    City = cityPropery[0];
                    PropertyType = cityPropery[1];

                    //3-Send to Predictor
                    var predictedRes = GetPredictionYearByYear(City, PropertyType, readModelFolderPath);

                    //4- Compare with current CSV
                    //Read CSV
                    var lastLine = File.ReadLines($"{readCSVSourcePath}/{City}-{PropertyType}.csv").Last().Split(',');
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

                    writer.WriteLine($"{toPrintLine}");
                }
            }

            float MAE_main = ErrorMainList.Average(); // Mean Absolute Error
            double RMSE_main = Math.Sqrt(ErrorMainList.Average(error => Math.Pow(error, 2))); // Root Mean Squared Error

            float MAE_lower = ErrorLowerList.Average(); // Mean Absolute Error
            double RMSE_lower = Math.Sqrt(ErrorLowerList.Average(error => Math.Pow(error, 2))); // Root Mean Squared Error

            float MAE_higher = ErrorHigherList.Average(); // Mean Absolute Error
            double RMSE_higher = Math.Sqrt(ErrorHigherList.Average(error => Math.Pow(error, 2))); // Root Mean Squared Error

            File.AppendAllText(totalCompareResultsTxt, $"{Environment.NewLine}{Path.GetFileNameWithoutExtension(compareResult)}{Environment.NewLine}");

            File.AppendAllText(totalCompareResultsTxt, $"MAE_main:{MAE_main.ToString("0.00")}{Environment.NewLine}");
            File.AppendAllText(totalCompareResultsTxt, $"RMSE_main:{RMSE_main.ToString("0.00")}{Environment.NewLine}");
            File.AppendAllText(totalCompareResultsTxt, $"MAE_lower:{MAE_lower.ToString("0.00")} {Environment.NewLine}");
            File.AppendAllText(totalCompareResultsTxt, $"RMSE_lower:{RMSE_lower.ToString("0.00")}{Environment.NewLine}");
            File.AppendAllText(totalCompareResultsTxt, $"MAE_higher:{MAE_higher.ToString("0.00")} {Environment.NewLine}");
            File.AppendAllText(totalCompareResultsTxt, $"RMSE_higher:{RMSE_higher.ToString("0.00")}{Environment.NewLine}");
            File.AppendAllText(totalCompareResultsTxt, $"-------------------------------");
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

            //int CurrentYear = DateTime.Now.Year;
            //PropertyPredictionDto propertyPredictionDto = new PropertyPredictionDto();

            //propertyPredictionDto.ConfidenceLowerBound.Add(CurrentYear, 0);
            //propertyPredictionDto.ConfidenceUpperBound.Add(CurrentYear, 0);
            //propertyPredictionDto.ForeCastIndex.Add(CurrentYear, 0);
            //for (int i = 1; i <= 5; i++)
            //{
            //    var lowerboundPercent = (result.ConfidenceLowerBound[28 * i - 1] - result.ConfidenceLowerBound[28 * (i - 1)]) / result.ConfidenceLowerBound[28 * i - 1];
            //    var upperBoundPercent = (result.ConfidenceUpperBound[28 * i - 1] - result.ConfidenceUpperBound[28 * (i - 1)]) / result.ConfidenceUpperBound[28 * i - 1];
            //    var indexPercent = (result.ForeCastIndex[28 * i - 1] - result.ForeCastIndex[28 * (i - 1)]) / result.ForeCastIndex[28 * i - 1];

            //    propertyPredictionDto.ConfidenceLowerBound.Add(CurrentYear + i, lowerboundPercent);
            //    propertyPredictionDto.ConfidenceUpperBound.Add(CurrentYear + i, upperBoundPercent);
            //    propertyPredictionDto.ForeCastIndex.Add(CurrentYear + i, indexPercent);
            //}

            //return propertyPredictionDto;
        }
    }
}