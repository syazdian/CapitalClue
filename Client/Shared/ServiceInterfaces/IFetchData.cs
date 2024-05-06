namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface IFetchData
{
    public Task<PropertyPredictionDto> GetPropertyPredicionPercent(string city, string propertyType);
    public Task<StockPredictionDto> GetStockPredicionPercent(string StockName, string Currency = "US");
    public Task<StockPredictionDto> GetStockPredicionPercent();

}