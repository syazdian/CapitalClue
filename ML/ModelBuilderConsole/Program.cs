using CapitalClue.Common.Models;
using CapitalClue.Common.Utilities;
using CapitalClue.Web.Server.ML.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
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
            string readFolderPath = @"C:\csv"; // Replace with your folder path
            List<string> csvFiles = new List<string>(Directory.EnumerateFiles(readFolderPath, "*.csv", SearchOption.AllDirectories));

            //2- Read each CSV file

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
                    propertyModelDto.PropertyType = cityPropery[1].Replace("homes", "houses");

                    //3-Send to Model Builder

                    await BuildPropertyModel(propertyModelDto);
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync($"ReadMapUpload: {ex.Message}");
                    throw;
                }
            }
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

        public static async Task BuildPropertyModel(PropertyModelDto property)
        {
            string ExportModelFileName = "PropertyModel.zip";
            string ExportDirectory = "C:/TrainedModels2";
            MLContext context;
            IDataView data;

            context = new MLContext();
            data = context.Data.LoadFromEnumerable<PropertyValueIndex>(property.PropertyValueIndices);
            ExportModelFileName = string.Format("{0}-{1}-{2}", property.City, property.PropertyType, ExportModelFileName);

            var pipline = context.Forecasting.ForecastBySsa(
             outputColumnName: nameof(PropertyPredictionEntity.ForeCastIndex),
             inputColumnName: nameof(PropertyValueIndex.Value),
             confidenceLevel: 0.95F,
             confidenceLowerBoundColumn: nameof(PropertyPredictionEntity.ConfidenceLowerBound),
             confidenceUpperBoundColumn: nameof(PropertyPredictionEntity.ConfidenceUpperBound),
             windowSize: 28,
             seriesLength: 28 * 24,
             trainSize: 28 * 24,
             horizon: 28 * 5);

            var model = pipline.Fit(data);

            context.Model.Save(model, data.Schema, $"{ExportDirectory}/{ExportModelFileName}");

            await Console.Out.WriteLineAsync($"successfully build Property Model{property.City}- {property.PropertyType}");
        }
    }
}