using Newtonsoft.Json;
using System.Text;

namespace CapitalClue.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : Controller
{
    //[HttpPost("TrainAndCreateModel")]
    //public async Task<IActionResult> TrainAndCreateModel(StockModelDto stockModelDto)
    //{
    //    try
    //    {
    //        var modelBulder = new StockModelBuilder(stockModelDto);
    //        modelBulder.Build();
    //        return Ok();
    //    }
    //    catch (Exception ex)
    //    {
    //        //_logger.LogError($"TrainAndMakeModelStock : {ex.Message}");
    //        throw;
    //    }
    //}

    [HttpGet("PredictYearByYear/{StockName}/{Currency}")]
    public async Task<IActionResult> PredictYearByYear([FromRoute] string StockName, string Currency)
    {
        var predictor = new StockModelDto() { Currency = Currency, StockName = StockName };
        var jsonContent = JsonConvert.SerializeObject(predictor);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        using (var client = new HttpClient())
        {
            var apiUrl = "http://localhost:7085/api/StockPredict"; // Your Azure Function URL
            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}