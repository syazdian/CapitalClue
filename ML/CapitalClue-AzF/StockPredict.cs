using CapitalClue.Common.Models;
using CapitalClue.Web.Server.Ml.Stock.StockPrediction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CapitalClue_AzF
{
    public class StockPredict
    {
        private readonly ILogger<StockPredict> _logger;

        public StockPredict(ILogger<StockPredict> logger)
        {
            _logger = logger;
        }

        [Function("StockPredict")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            try
            {
                string requestBody;
                using (StreamReader reader = new StreamReader(req.Body))
                {
                    requestBody = await reader.ReadToEndAsync();
                }

                var stockModel = JsonConvert.DeserializeObject<StockModelDto>(requestBody);

                if (stockModel is null || stockModel.Currency is null || stockModel.StockName is null)
                {
                    return new BadRequestObjectResult(stockModel);
                }

                var predictor = new StockPredictor(stockModel.StockName, stockModel.Currency);
                var result = predictor.GetPredictionYearByYear();

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}