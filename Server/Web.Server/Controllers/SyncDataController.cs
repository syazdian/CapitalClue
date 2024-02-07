using CapitalClue.Web.Server.Model;

namespace CapitalClue.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SyncDataController : Controller
{
    private readonly ServerDbRepository _dbRepo;
    private readonly ILogger _logger;

    public SyncDataController(ServerDbRepository dbRepo, ILogger<SyncDataController> logger)//, DatabaseGenerator dbGenerator)
    {
        _dbRepo = dbRepo;
        _logger = logger;
        // _dbGenerator = dbGenerator;
    }

    [HttpPost("SyncChangesStaple")]
    public async Task<IActionResult> SyncChangesStaple(List<StaplesSourceDto> stapleSourceChanges)
    {
        try
        {
            await _dbRepo.SyncStapleSourceChanges(stapleSourceChanges);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"SyncChangesStaple : {ex.Message}");
            throw;
        }
    }


    [HttpPost("DeleteItems")]
    public async Task<IActionResult> DeleteItems(ToDeleteItemsDto toDeleteItemsDto)
    {
        try
        {
            foreach (var item in toDeleteItemsDto.BellIds)
                await _dbRepo.DeleteReconBell(item);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"DeleteBellSources : {ex.Message}");
            throw;
        }
    }

    [HttpDelete("DeleteRecoStaple")]
    public async Task<IActionResult> DeleteRecoStaple(long Id)
    {
        try
        {
            await _dbRepo.DeleteReconStaple(Id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"DeleteRecoStaple : {ex.Message}");
            throw;
        }
    }

    [HttpPost("SyncChangesBell")]
    public async Task<IActionResult> SyncChangesBell(List<BellSourceDto> bellSourceChanges)
    {
        try
        {
            await _dbRepo.SyncBellSourceChanges(bellSourceChanges);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"SyncChangesBell : {ex.Message}");
            throw;
        }
    }

    [HttpGet("GetLatestChangedStaplesSourceItemsByDate/{dateTime}/{user}")]
    public async Task<IActionResult> GetLatestChangedStaplesSourceItemsByDate([FromRoute] DateTime dateTime, string user)
    {
        try
        {
            var res = await _dbRepo.GetStaplesSourceLatestReconciledDate(dateTime, user);
            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetLatestChangedStaplesSourceItemsByDate : {ex.Message}");
            throw;
        }
    }

    [HttpGet("GetLatestChangedBellSourceItemsByDate/{dateTime}/{user}")]
    public async Task<IActionResult> GetLatestChangedBellSourceItemsByDate([FromRoute] DateTime dateTime, string user)
    {
        try
        {
            var res = await _dbRepo.GetBellSourceByLatestReconciledDate(dateTime, user);
            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetLatestChangedBellSourceItemsByDate : {ex.Message}");
            throw;
        }
    }
}