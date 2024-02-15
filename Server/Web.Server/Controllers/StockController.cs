using CapitalClue.Web.Server.Ml.Stock.ModelBuilder;

namespace CapitalClue.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : Controller
{
    [HttpPost("TrainAndMakeModel")]
    public async Task<IActionResult> TrainAndMakeModel(StockModelDto stockModelDto)
    {
        try
        {
            var modelBulder = new ModelBuilder(stockModelDto);
            return Ok();
        }
        catch (Exception ex)
        {
            //_logger.LogError($"TrainAndMakeModelStock : {ex.Message}");
            throw;
        }
    }

    [HttpGet("Predict")]
    public async Task<IActionResult> Predict()
    {
        try
        {
            
            return Ok();
        }
        catch (Exception ex)
        {
            //_logger.LogError($"PredictStock : {ex.Message}");
            throw;
        }
    }
}
