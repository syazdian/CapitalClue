using Bell.Reconciliation.Common.Models;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

namespace Bell.Reconciliation.Frontend.Desktop.Models;

public class StateContainer : IStateContainer
{
    public IEnumerable<OnlyInBellDto> bellSources { get; set; }
    public IEnumerable<OnlyInStaplesDto> staplesSources { get; set; }
    public IEnumerable<CompareBellStapleDto> compareBellStaple { get; set; }

    public FilterItemDto filterItemDto { get; set; } = new();

    public string UserId { get; set; } = "User";
    public string UserName { get; set; } = "User";
    public string Enviornment { get; set; }
    public DateTime? SmallestDateLocalDb { get; set; }
    public DateTime? LatestDateLocalDb { get; set; }

    public List<string> StoreNumberDisplay { get; set; } = new();

}
