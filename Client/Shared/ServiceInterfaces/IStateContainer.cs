namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface IStateContainer
{
    public List<ChartDataItem> revenueProperty { get; set; }
    public List<ChartDataItem> revenuePropertyUpperBound { get; set; }
    public List<ChartDataItem> revenuePropertyLowerBound { get; set; }
}