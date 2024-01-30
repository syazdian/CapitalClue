namespace Bell.Reconciliation.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoadDetailController : Controller
{
    private readonly ServerDbRepository _dbRepo;

    public LoadDetailController(ServerDbRepository dbRepo)//, DatabaseGenerator dbGenerator)
    {
        _dbRepo = dbRepo;
    }

    [HttpGet("LoadDeatailByPhone/{phonenumber}")]
    public async Task<IActionResult> FetchCountServerDatabase([FromRoute] string phonenumvber = "1")
    {
        var bellStapleCountDto = await _dbRepo.CountBellStapleRows();
        return Ok(bellStapleCountDto);
    }

    [HttpGet("LoadDeatailByImei/{imei}")]
    public async Task<IActionResult> LoadDeatailByImei([FromRoute] string iemi = "1")
    {
        var bellStapleCountDto = await _dbRepo.CountBellStapleRows();
        return Ok(bellStapleCountDto);
    }

    [HttpGet("LoadDeatailByOrderNumber/{ordernumber}")]
    public async Task<IActionResult> LoadDeatailByOrderNumber([FromRoute] string ordernumber = "1")
    {
        var bellStapleCountDto = await _dbRepo.CountBellStapleRows();
        return Ok(bellStapleCountDto);
    }
}