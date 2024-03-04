namespace CapitalClue.Frontend.Web.Models;

public class StateContainer : IStateContainer
{
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
}