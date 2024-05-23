using CapitalClue.Common.Models;
using CapitalClue.Web.Server.Ml.Property.ModelBuilder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CapitalClue_AzF
{
    public class PropertyTrainCreateModel
    {
        private readonly ILogger<PropertyTrainCreateModel> _logger;

        public PropertyTrainCreateModel(ILogger<PropertyTrainCreateModel> logger)
        {
            _logger = logger;
        }

        [Function("PropertyTrainCreateModel")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            try
            {
                string requestBody;
                using (StreamReader reader = new StreamReader(req.Body))
                {
                    requestBody = await reader.ReadToEndAsync();
                }

                var propertyModel = JsonConvert.DeserializeObject<PropertyModelDto>(requestBody);

                var modelBulder = new PropertyModelBuilder(propertyModel);
                modelBulder.Build();
                return new OkObjectResult($"successfully build Property Model{propertyModel.City}- {propertyModel.PropertyType}");
            }
            catch (Exception ex)
            {
                //_logger.LogError($"TrainAndMakeModelStock : {ex.Message}");
                throw;
            }
        }
    }
}