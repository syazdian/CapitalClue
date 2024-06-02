using CapitalClue.Frontend.Shared.Models;

namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface IStateContainer
{
    public List<ChartDataItem> revenueProperty { get; set; }
    public List<ChartDataItem> revenuePropertyUpperBound { get; set; }
    public List<ChartDataItem> revenuePropertyLowerBound { get; set; }

    public Story SelectedStory { get; set; }
    public int IndexPage { get; set; }

    public List<ChartDataItem> revenueStock { get; set; }
    public List<ChartDataItem> revenueStockUpperBound { get; set; }
    public List<ChartDataItem> revenueStockLowerBound { get; set; }

    public double PropertyDownpayment { get; set; }
    public double PropertyMonthlyPayment { get; set; }
    public double PropertyMonthlyRent { get; set; }
    public double StockContribution { get; set; }
    public double StockDownpayment { get; set; }

    public double houseCost { get; set; }
    public double interestRate { get; set; }
    public double mortgagePayment { get; set; }
    public double propertyTax { get; set; }
    public double homeInsurance { get; set; }
    public double maintananceFee { get; set; }
    public int mortagePaymentPeriod { get; set; }

    public string PropertyType { get; set; }
    public string City { get; set; }

    public bool IsFirstTimeHomeBuyer { get; set; }
    public bool IsMarried { get; set; }
    public string UserFirstName { get; set; }
    public int YearToRetire { get; set; }
    public int Age { get; set; }
    public int ChildCount { get; set; }
    public double Income { get; set; }
}