namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface IFetchData
{
    public Task<int> FetchDataFromServerDb(DateTime? startDate = null, DateTime? endDate = null);

    public Task GenerateDataInServerDb();

    Task<string> GetDetails(string type, string value);

    Task<List<RefDealerDto>> GetRefDealersAsync();

    //Task<string> GetDetailBySerialNumber(string sn);

    //Task<string> GetDetailByOrderNumber(string on);
}