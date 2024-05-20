using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CreateCSV
{
    internal class Program
    {
        public static List<DateOnly> dates { get; set; } = new();
        public static List<List<double>> prices { get; set; } = new();
        public static List<string> houseTypes { get; set; } = new();

        public static void Main(string[] args)
        {
            Dictionary<string, string> cityUrl = new();

            cityUrl.Add("Vaughan", "https://vaughan.listing.ca/real-estate-price-history.htm");
            cityUrl.Add("RichmondHill", "https://richmond-hill.listing.ca/real-estate-price-history.htm");
            cityUrl.Add("Toronto", "https://toronto.listing.ca/real-estate-price-history.htm");
            cityUrl.Add("Mississauga", "https://mississauga.listing.ca/real-estate-price-history.htm");
            cityUrl.Add("Markham", "https://markham.listing.ca/real-estate-price-history.htm");
            cityUrl.Add("Brampton", "https://brampton.listing.ca/real-estate-price-history.htm");

            foreach (var city in cityUrl)
            {
                ExtractArraysAsync(city.Value).Wait();
                CreateCsvFiles(city.Key);
            }
        }

        public static async Task ExtractArraysAsync(string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                string content = await client.GetStringAsync(url);

                string datesPattern = @"\$svg\.xDates = (\[[^\]]+\])";
                string pricesPattern = @"\$svg\.xPrices = (\[\[.*\]\])";
                string housenames = @"\$svg\.graphNames = (\[[^\]]+\])";

                Match datesMatch = Regex.Match(content, datesPattern);
                Match pricesMatch = Regex.Match(content, pricesPattern);
                Match houseMatch = Regex.Match(content, housenames);

                if (datesMatch.Success)
                {
                    string datesArray = datesMatch.Groups[1].Value;
                    datesArray = datesArray.Replace("'", "\"");
                    List<string> dateString = JsonSerializer.Deserialize<List<string>>(datesArray);
                    dates = dateString.Select(date => DateOnly.Parse(date)).ToList();
                }

                if (pricesMatch.Success)
                {
                    string pricesArray = pricesMatch.Groups[1].Value;
                    pricesArray = pricesArray.Replace("'", "\"");
                    List<List<string>> priceString = JsonSerializer.Deserialize<List<List<string>>>(pricesArray);
                    prices = priceString.Select(subArray => subArray.Select(price => double.Parse(price.Replace(",", ""))).ToList()).ToList();
                }
                if (houseMatch.Success)
                {
                    string houseArray = houseMatch.Groups[1].Value;
                    houseArray = houseArray.Replace("'", "\"");
                    houseTypes = JsonSerializer.Deserialize<List<string>>(houseArray);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void CreateCsvFiles(string cityName)
        {
            if (houseTypes.Count != prices.Count)
            {
                throw new ArgumentException("The number of house types must be equal to the number of price lists.");
            }

            try
            {
                // Create the directory if it doesn't exist
                Directory.CreateDirectory($"C:\\csv");

                for (int i = 0; i < houseTypes.Count; i++)
                {
                    string fileName = Path.Combine($"C:\\csv", $"{cityName}-{houseTypes[i]}.csv");

                    using (StreamWriter writer = new StreamWriter(fileName))
                    {
                        // Write the header row
                        writer.WriteLine("Date,Price");

                        // Write each data row
                        for (int j = 0; j < dates.Count; j++)
                        {
                            writer.WriteLine(
                                $"{dates[j]:yyyy-MM-dd},{prices[i][j]}"
                            );
                        }
                    }

                    Console.WriteLine($"CSV file created: {fileName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating CSV files: {ex.Message}");
            }
        }
    }
}