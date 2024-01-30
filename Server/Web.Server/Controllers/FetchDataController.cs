namespace CapitalClue.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FetchDataController : Controller
{
    private readonly ServerDbRepository _dbRepo;
    private readonly CallApi _Api;
    private readonly ILogger _logger;

    public FetchDataController(ServerDbRepository dbRepo, CallApi api, ILogger<FetchDataController> logger)//, DatabaseGenerator dbGenerator)
    {
        _dbRepo = dbRepo;
        _Api = api;
        _logger = logger;
    }

    [HttpGet("FetchCountServerDatabase/{startDateTicks}/{endDateTicks}")]
    public async Task<IActionResult> FetchCountServerDatabase([FromRoute] long? startDateTicks = null, long? endDateTicks = null)
    {
        try
        {
            DateTime? startDate = null;
            if (startDateTicks.HasValue && startDateTicks.Value > 0)
                startDate = new DateTime(startDateTicks.Value);

            DateTime? endDate = null;
            if (endDateTicks.HasValue && endDateTicks.Value > 0)
                endDate = new DateTime(endDateTicks.Value);

            var bellStapleCountDto = await _dbRepo.CountBellStapleRows(startDate, endDate);
            return Ok(bellStapleCountDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"FetchCountServerDatabase : {ex.Message}");

            throw;
        }
    }

    [HttpGet("GetBellSourceItems/{startCount}/{endCount}/{startDateTicks}/{endDateTicks}")]
    public async Task<IActionResult> GetBellSourceItems([FromRoute] int startCount = 1, int endCount = 1, long? startDateTicks = null, long? endDateTicks = null)
    {
        try
        {
            DateTime? startDate = null;
            if (startDateTicks.HasValue && startDateTicks.Value > 0)
                startDate = new DateTime(startDateTicks.Value);

            DateTime? endDate = null;
            if (endDateTicks.HasValue && endDateTicks.Value > 0)
                endDate = new DateTime(endDateTicks.Value);

            var items = await _dbRepo.GetBellSource(startCount: startCount, endCount: endCount, startDate: startDate, endDate: endDate);
            return Ok(items);
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetBellSourceItems : {ex.Message}");
            throw;
        }
    }

    [HttpGet("GetStaplesSourceItems/{startCount}/{endCount}/{startDateTicks}/{endDateTicks}")]
    public async Task<IActionResult> GetStaplesSourceItems([FromRoute] int startCount = 1, int endCount = 1, long? startDateTicks = null, long? endDateTicks = null)
    {
        try
        {
            DateTime? startDate = null;
            if (startDateTicks.HasValue && startDateTicks.Value > 0)
                startDate = new DateTime(startDateTicks.Value);

            DateTime? endDate = null;
            if (endDateTicks.HasValue && endDateTicks.Value > 0)
                endDate = new DateTime(endDateTicks.Value);

            var items = await _dbRepo.GetStaplesSource(startCount: startCount, endCount: endCount, startDate: startDate, endDate: endDate);
            return Ok(items);
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetStaplesSourceItems : {ex.Message}");
            throw;
        }
    }

    [HttpGet("GetDetails/{title}/{value}")]
    public async Task<IActionResult> GetDetails([FromRoute] string title, string value)
    {
        try
        {
            var items = await _Api.GetDetails(title, value);
            return Ok(items);
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetDetails : {ex.Message}");
            throw;
        }
    }

    [HttpGet("GetStores")]
    public async Task<IActionResult> GetStores()
    {
        try
        {
            var items = await _dbRepo.GetStores();
            return Ok(items);
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetStores : {ex.Message}");
            throw;
        }
    }

    [HttpGet("GetDealers")]
    public async Task<IActionResult> GetDealers()
    {
        try
        {
            var items = await _dbRepo.GetDealers();
            return Ok(items);
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetDealers : {ex.Message}");
            throw;
        }
    }
}