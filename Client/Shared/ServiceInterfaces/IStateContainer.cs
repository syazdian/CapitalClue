namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface IStateContainer
{
    public IEnumerable<OnlyInBellDto> bellSources { get; set; }
    public IEnumerable<OnlyInStaplesDto> staplesSources { get; set; }
    public IEnumerable<CompareBellStapleDto> compareBellStaple { get; set; }

    public FilterItemDto filterItemDto { get; set; }

    public string UserId { get; set; }
    public string UserName { get; set; }

    public string Enviornment { get; set; }
    public DateTime? SmallestDateLocalDb { get; set; }
    public DateTime? LatestDateLocalDb { get; set; }
    public List<string> StoreNumberDisplay { get; set; }

}