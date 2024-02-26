using CapitalClue.Web.Server.Ml.Property.PropertyPrediction;
using CapitalClue.Web.Server.Ml.Stock.ModelBuilder;
using CapitalClue.Web.Server.Ml.Stock.StockPrediction;

namespace CapitalClue.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : Controller
{
    [HttpPost("TrainAndCreateModel")]
    public async Task<IActionResult> TrainAndCreateModel(StockModelDto stockModelDto)
    {
        try
        {
            var modelBulder = new StockModelBuilder(stockModelDto);
            modelBulder.Build();
            return Ok();
        }
        catch (Exception ex)
        {
            //_logger.LogError($"TrainAndMakeModelStock : {ex.Message}");
            throw;
        }
    }

    [HttpGet("PredictYearByYear/{City}/{PropertyType}")]
    public async Task<IActionResult> PredictYearByYear([FromRoute] string City, string PropertyType)
    {
        try
        {
            var predictor = new StockPredictor(City, PropertyType);
            var result = predictor.GetPredictionYearByYear();
            return Ok(result);
        }
        catch (Exception ex)
        {
            //_logger.LogError($"PredictStock : {ex.Message}");
            throw;
        }
    }
}
