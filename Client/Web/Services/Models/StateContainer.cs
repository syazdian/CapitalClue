using CapitalClue.Frontend.Shared.Models;

namespace CapitalClue.Frontend.Web.Models;

public class StateContainer : IStateContainer
{
    public Story selectedStory { get; set; }

    public List<ChartDataItem> revenueProperty { get; set; }
    public List<ChartDataItem> revenuePropertyUpperBound { get; set; }
    public List<ChartDataItem> revenuePropertyLowerBound { get; set; }

    public List<ChartDataItem> revenueStock { get; set; }
    public List<ChartDataItem> revenueStockUpperBound { get; set; }
    public List<ChartDataItem> revenueStockLowerBound { get; set; }

    public Story SelectedStory { get; set; }
    public int IndexPage { get; set; }

    public double PropertyDownpayment { get; set; }
    public double PropertyMonthlyPayment { get; set; }
    public double PropertyMonthlyRent { get; set; }

    public double StockContribution { get; set; }
    public double StockDownpayment { get; set; }

    public double houseCost { get; set; } = 0;
    public double interestRate { get; set; } = 0;
    public double mortgagePayment { get; set; } = 0;
    public double propertyTax { get; set; } = 0;
    public double homeInsurance { get; set; } = 0;
    public double maintananceFee { get; set; } = 0;
    public int mortagePaymentPeriod { get; set; } = 0;

    public string PropertyType { get; set; }
    public string City { get; set; }

    public bool IsFirstTimeHomeBuyer { get; set; }
    public bool IsMarried { get; set; }
    public bool? IsMale { get; set; } = null;
    public int ChildCount { get; set; } = 0;
    public string UserFirstName { get; set; }
    public int UserAge { get; set; }
}