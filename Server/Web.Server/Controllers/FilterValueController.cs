namespace CapitalClue.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilterValueController : Controller
{
    public FilterValueController()
    {
    }

    [HttpGet("GetHello")]
    public async Task<IActionResult> GetHello()
    {
        string msg = "HELLO FROM BKND";
        return Ok(msg);
    }
}