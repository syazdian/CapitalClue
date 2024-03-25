using CapitalClue.Common.Models;
using CapitalClue.Frontend.Shared.ServiceInterfaces;

namespace Bell.Reconciliation.Frontend.Desktop.Models;

public class StateContainer : IStateContainer
{
    public List<ChartDataItem> revenueProperty { get; set; }
    public List<ChartDataItem> revenuePropertyUpperBound { get; set; }
    public List<ChartDataItem> revenuePropertyLowerBound { get; set; }
}
