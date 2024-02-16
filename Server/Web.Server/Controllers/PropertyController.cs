using CapitalClue.Web.Server.Ml.Property.ModelBuilder;
using CapitalClue.Web.Server.Ml.Property.PropertyPrediction;
using CapitalClue.Web.Server.Ml.Stock.StockPrediction;

namespace CapitalClue.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : Controller
{
    [HttpPost("TrainAndMakeModel")]
    public async Task<IActionResult> TrainAndMakeModel(PropertyModelDto propertyModelDto)
    {
        try
        {
            var modelBulder = new PropertyModelBuilder(propertyModelDto);
            modelBulder.Build();
            return Ok();
        }
        catch (Exception ex)
        {
            //_logger.LogError($"TrainAndMakeModelStock : {ex.Message}");
            throw;
        }
    }

    [HttpGet("Predict/{City}/{PropertyType}")]
    public async Task<IActionResult> Predict([FromRoute] string City, string PropertyType)
    {
        try
        {
            var predictor = new PropertyPrediction(City, PropertyType);
            var result = predictor.GetPrediction();
            return Ok(result);
        }
        catch (Exception ex)
        {
            //_logger.LogError($"PredictStock : {ex.Message}");
            throw;
        }
    }
}
