namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface IFilterService
{
    public Task<FilterItemsDisplay> GetFilterItems();

    public string GetFilterJson();

    public Task<string> GetHello();
}