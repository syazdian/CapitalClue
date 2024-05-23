using Newtonsoft.Json;
using System.Text;

namespace CapitalClue.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : Controller
{
    private string baseAddress;
    private string code;

    public StockController(UrlKeeper url)
    {
        baseAddress = url.BaseUrl;
        code = url.code;
    }

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
            var apiUrl = $"{baseAddress}api/StockPredict/{code}";
            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Ok(responseContent);
            }

            return BadRequest(response);
        }
    }
}