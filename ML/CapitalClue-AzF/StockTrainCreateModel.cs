using CapitalClue.Common.Models;
using CapitalClue.Web.Server.Ml.Property.ModelBuilder;
using CapitalClue.Web.Server.Ml.Stock.ModelBuilder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CapitalClue_AzF
{
    public class StockTrainCreateModel
    {
        private readonly ILogger<StockTrainCreateModel> _logger;

        public StockTrainCreateModel(ILogger<StockTrainCreateModel> logger)
        {
            _logger = logger;
        }

        [Function("StockTrainCreateModel")]
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

                var modelBuilder = new StockModelBuilder(stockModel);
                modelBuilder.Build();
                return new OkObjectResult($"successfully build Property Model{stockModel.StockName}-{stockModel.Currency} ");
            }
            catch (Exception ex)
            {
                //_logger.LogError($"TrainAndMakeModelStock : {ex.Message}");
                throw;
            }
        }
    }
}