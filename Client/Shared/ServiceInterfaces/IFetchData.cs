namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface IFetchData
{
    public Task<PropertyPredictionDto> GetPropertyPredicionPercent(string city, string propertyType);

}