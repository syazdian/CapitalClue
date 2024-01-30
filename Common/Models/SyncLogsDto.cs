using Microsoft.EntityFrameworkCore;

namespace CapitalClue.Common.Models;

public class SyncLogsDto
{
    public int Id { get; set; }
    public DateTime StartSync { get; set; }
    public DateTime? EndSync { get; set; }
    public bool? Success { get; set; }
}
