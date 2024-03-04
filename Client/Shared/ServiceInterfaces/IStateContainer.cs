namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface IStateContainer
{
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
}