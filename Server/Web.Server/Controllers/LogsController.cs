using CapitalClue.Web.Server.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace CapitalClue.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogsController : ControllerBase
{
    private readonly ServerDbRepository _dbRepo;
    private readonly ILogger _logger;

    public LogsController(ServerDbRepository dbRepo, ILogger<LogsController> logger)
    {
        _dbRepo = dbRepo;
        _logger = logger;
    }

    [HttpPost("SyncUserId")]
    public async Task<IActionResult> SyncUserId(UserIdDto UserName)
    {
        try
        {
            UserId.UserName = UserName.UserName;
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"SyncUserId : {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    // GET: api/Logs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ErrorLogDto>>> GetLogs()
    {
        return await _dbRepo.GetAllLogs();
    }

    // GET: api/Logs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ErrorLogDto>> GetLog(int id)
    {
        var log = await _dbRepo.GetLogById(id);

        if (log == null)
        {
            return NotFound();
        }

        return Ok(log);
    }

    // POST: api/Logs
    [HttpPost]
    public async Task<ActionResult> PostLog(ErrorLogDto log)
    {
        try
        {
            await _dbRepo.InsertErrorLog(log);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"PostLog: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("InsertErrorLogs")]
    public async Task<ActionResult> InsertErrorLogs(List<ErrorLogDto> Errors)
    {
        try
        {
            if (await _dbRepo.InsertErrorLogs(Errors))
                return Ok();
            else { return StatusCode(500, "InsertErrorLogs not successfull"); }
        }
        catch (Exception ex)
        {
            _logger.LogError($"InsertErrorLogs: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    //// DELETE: api/Logs/5
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteLog(long id)
    //{
    //    var log = await _context.Logs.FindAsync(id);
    //    if (log == null)
    //    {
    //        return NotFound();
    //    }

    //    _context.Logs.Remove(log);
    //    await _context.SaveChangesAsync();

    //    return NoContent();
    //}

    //private bool LogExists(long id)
    //{
    //    return _context.Logs.Any(e => e.LogId == id);
    //}
}