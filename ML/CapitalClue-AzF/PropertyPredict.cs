using CapitalClue.Common.Models;
using CapitalClue.Web.Server.Ml.Property.PropertyPrediction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CapitalClue_AzF
{
    public class PropertyPredict
    {
        private readonly ILogger<PropertyPredict> _logger;

        public PropertyPredict(ILogger<PropertyPredict> logger)
        {
            _logger = logger;
        }

        [Function("PropertyPredict")]
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

                if (propertyModel is null || propertyModel.City is null || propertyModel.PropertyType is null)
                {
                    return new BadRequestObjectResult(propertyModel);
                }

                var predictor = new PropertyPrediction(propertyModel.City, propertyModel.PropertyType);

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