using Azure;

using Newtonsoft.Json;
using System.Text;

namespace CapitalClue.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : Controller
{
    //[HttpPost("TrainAndCreateModel")]
    //public async Task<IActionResult> TrainAndCreateModel(PropertyModelDto propertyModelDto)
    //{
    //    try
    //    {
    //        var modelBulder = new PropertyModelBuilder(propertyModelDto);
    //        modelBulder.Build();
    //        return Ok();
    //    }
    //    catch (Exception ex)
    //    {
    //        //_logger.LogError($"TrainAndMakeModelStock : {ex.Message}");
    //        throw;
    //    }
    //}

    [HttpGet("PredictYearByYear/{City}/{PropertyType}")]
    public async Task<IActionResult> PredictYearByYear([FromRoute] string City, string PropertyType)
    {
        try
        {
            var predictor = new PropertyModelDto() { City = City, PropertyType = PropertyType };
            var jsonContent = JsonConvert.SerializeObject(predictor);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var apiUrl = "http://localhost:7085/api/PropertyPredict"; // Your Azure Function URL
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return Ok(responseContent);
                }

                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            //_logger.LogError($"PredictStock : {ex.Message}");
            throw;
        }
    }
}