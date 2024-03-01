
namespace CapitalClue.Frontend.Web.Models;

public class StateContainer : IStateContainer
{
    public List<ChartDataItem> revenueProperty { get; set; }
    public List<ChartDataItem> revenuePropertyUpperBound { get; set; }
    public List<ChartDataItem> revenuePropertyLowerBound { get; set; }
}