using CapitalClue.Common.Models;
using CapitalClue.Common.Utilities;
using CapitalClue.Web.Server.ML.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using System.Data;
using System.IO;
using System.Text;

namespace PropertyModelBuilderConsole
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            //1- Read list of CSV files
            //Read CSV Files:
            string readFolderPath = @"C:\Users\Sharif\Desktop\_Capital Clue\archive"; // Replace with your folder path
            List<string> csvFiles = new List<string>(Directory.EnumerateFiles(readFolderPath, "*.csv", SearchOption.AllDirectories));

            //2- Read each CSV file

            foreach (string file in csvFiles)
            {
                StockModelDto stckModel = new StockModelDto();

                string fileName = Path.GetFileNameWithoutExtension(file);
                Console.WriteLine($"Reading file: {fileName}");
                // Read the contents of the file
                string csvValue = File.ReadAllText(file);
                if (csvValue is null) return;
                try
                {
                    stckModel.StockValueIndices = await ReadCSVFile(csvValue);
                    if (stckModel.StockValueIndices is null)
                    {
                        return;
                    }
                    var stckCurrnecy = fileName.Replace(".csv", "").Split('-');

                    stckModel.StockName = stckCurrnecy[0];
                    stckModel.Currency = stckCurrnecy[1];

                    //3-Send to Model Builder

                    await BuildStockModel(stckModel);
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync($"ReadMapUpload: {ex.Message}");
                    throw;
                }
            }
        }

        public static async Task<List<StockValueIndex>> ReadCSVFile(string csvFile)
        {
            try
            {
                List<string> errorMessages;

                List<StockValueIndex> StockValueIndices = new();
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
                if (Array.IndexOf(columnNames, "date") == -1)
                {
                    errorMessages.Add("column name DATE not found");
                    return null;
                }
                if (Array.IndexOf(columnNames, "open") == -1)
                { errorMessages.Add("column name Price not found"); return null; }

                int lineNumnber = 0;
                foreach (var line in lines)
                {
                    errorMessages = new List<string>();

                    StockValueIndex stockValueIndex = new();
                    string[] values = line.Split(',');

                    values[columnNames.Length - 1] = values[columnNames.Length - 1].TrimEnd('\r');
                    values = values.Select(s => s.Trim()).ToArray();

                    if (!DateTime.TryParse(values[Array.IndexOf(columnNames, "date")].Trim(), out DateTime transactionDate))
                    {
                        errorMessages.Add("TRANSACTION DATE is invalid");
                    }
                    else
                    {
                        stockValueIndex.DateTime = transactionDate;
                    }

                    if (float.TryParse(values[Array.IndexOf(columnNames, "open")].Trim(), out float price))
                    {
                        stockValueIndex.Value = price;
                    }

                    StockValueIndices.Add(stockValueIndex);
                    lineNumnber++;
                    await Task.Delay(1);
                }

                return StockValueIndices;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"ReadCSVFile: {ex.Message}");
                throw;
            }
        }

        public static async Task BuildStockModel(StockModelDto stockData)
        {
            string ExportModelFileName = "StockModel.mlnet";
            string ExportDirectory = "C:/TrainedModels3";
            MLContext context;
            IDataView data;

            context = new MLContext();
            data = context.Data.LoadFromEnumerable<StockValueIndex>(stockData.StockValueIndices);
            ExportModelFileName = string.Format("{0}-{1}-{2}", stockData.StockName, stockData.Currency, ExportModelFileName);

            var pipline = context.Forecasting.ForecastBySsa(
                  outputColumnName: nameof(StockPredictionEntiy.ForeCastIndex),
                  inputColumnName: nameof(StockValueIndex.Value),
                  confidenceLevel: 0.95F,
                  confidenceLowerBoundColumn: nameof(StockPredictionEntiy.ConfidenceLowerBound),
                  confidenceUpperBoundColumn: nameof(StockPredictionEntiy.ConfidenceUpperBound),
                  windowSize: 252,
                  seriesLength: 252 * 10,
                  trainSize: 252 * 10,
                  horizon: 252 * 5
                  //     rankSelectionMethod: RankSelectionMethod.

                  );

            var model = pipline.Fit(data);
            context.Model.Save(model, data.Schema, $"{ExportDirectory}/{ExportModelFileName}");
            await Console.Out.WriteLineAsync($"successfully build stock Model{stockData.StockName}");
        }
    }
}