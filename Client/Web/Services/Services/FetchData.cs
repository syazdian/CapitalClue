using Microsoft.Extensions.Logging;

namespace CapitalClue.Frontend.Web.Services;

public class FetchData : IFetchData
{
    private readonly HttpClient _httpClient;
    private readonly ILocalDbRepository _localDb;
    private readonly ILogger _logger;
    private readonly string baseAddress;
    private const int packageSize = 1000;

    //TODO: THIS IS FOR DEV IT SHOULD BE BELL AND STAPLES COUNT
    //  private const int maximumDownload = 1000;

    public FetchData(HttpClient httpClient, ILocalDbRepository localDb, UrlKeeper url, ILogger<FetchData> logger)
    {
        _httpClient = httpClient;
        _localDb = localDb;
        // baseAddress = configuration["baseaddress"];
        baseAddress = url.BaseUrl;
        _logger = logger;
    }

    public async Task<int> FetchDataFromServerDb(DateTime? startDate = null, DateTime? endDate = null)
    {
        try
        {

            string startDateVar = startDate?.Ticks.ToString() ?? "0";
            string endDateVar = endDate?.Ticks.ToString() ?? "0";

            var url = $"{baseAddress}api/FetchData/FetchCountServerDatabase/{startDateVar}/{endDateVar}";


            var dbcount = await _httpClient.GetFromJsonAsync<BellStapleCountDto>(url);
            if (dbcount is null) throw new ArgumentNullException(nameof(dbcount));

            if (dbcount.StaplesCount > 0)
            {
                int startStapleCount = 1;
                int lastStapleCount = packageSize;
                do
                {
                    url = $"{baseAddress}api/FetchData/GetStaplesSourceItems/{startStapleCount}/{lastStapleCount}/{startDateVar}/{endDateVar}";

                    var staplesList = await _httpClient.GetFromJsonAsync<List<StaplesSourceDto>>(url);

                    if (staplesList is not null)
                        await _localDb.InsertStaplesToLocalDbAsync(staplesList);
                    else throw new ArgumentNullException(nameof(staplesList));
                    startStapleCount = lastStapleCount + 1;
                    lastStapleCount = lastStapleCount + packageSize;
                    if (lastStapleCount > dbcount.StaplesCount)
                        lastStapleCount = dbcount.StaplesCount;
                    await Task.Delay(1);
                } while (startStapleCount <= dbcount.StaplesCount);//maximumDownload );
            }
            await Task.Delay(1);
            if (dbcount.BellCount > 0)
            {
                int startBellCount = 1;
                int lastBellCount = packageSize;
                do
                {
                    url = $"{baseAddress}api/FetchData/GetBellSourceitems/{startBellCount}/{lastBellCount}/{startDateVar}/{endDateVar}";

                    var bellList = await _httpClient.GetFromJsonAsync<List<BellSourceDto>>(url);
                    if (bellList is not null)
                        await _localDb.InsertBellSourceToLocalDbAsync(bellList);
                    else throw new ArgumentNullException(nameof(bellList));
                    startBellCount = lastBellCount + 1;
                    lastBellCount = lastBellCount + packageSize;
                    if (lastBellCount > dbcount.BellCount)
                        lastBellCount = dbcount.BellCount;
                    await Task.Delay(1);
                } while (startBellCount <= dbcount.BellCount); // maximumDownload);
            }
            await Task.Delay(1);

            //url = $"{baseAddress}api/FetchData/GetStores";
            //var storeList = await _httpClient.GetFromJsonAsync<List<StoreDto>>(url);
            //if (storeList is not null)
            //    await _localDb.InsertStoreSourceToLocalDbAsync(storeList);
            //else throw new ArgumentNullException(nameof(storeList));

            if (dbcount.BellCount + dbcount.StaplesCount == 0)
            {
                return 0;
            }

            await _localDb.GetTotalTables();
            await _localDb.InsertFetchHistory(startDate, endDate);

            var fetchHistories = await _localDb.GetFetchHistory();
            if (fetchHistories.Count == 1)
            {
                SyncLogsDto syncLogsDto = new SyncLogsDto()
                {
                    Success = true,
                    EndSync = DateTime.UtcNow,
                    StartSync = DateTime.UtcNow,
                };
                await Task.Delay(1);
                await _localDb.InsertSyncLog(syncLogsDto);
            }


            return dbcount.BellCount + dbcount.StaplesCount;
        }
        catch (Exception ex)
        {
            _logger.LogError($"FetchDataFromServerDb: {ex.Message}");

            throw;
        }
    }

    public async Task GenerateDataInServerDb()
    {
        try
        {
            var res = await _httpClient.GetStringAsync($"{baseAddress}api/syncData/GenerateServerDb/1000/y/10");

            if (res is "Done") { }
        }
        catch (Exception ex)
        {
            _logger.LogError($"GenerateDataInServerDb: {ex.Message}");

            throw;
        }
    }

    public async Task<string> GetDetails(string title, string value)
    {
        var res = await _httpClient.GetStringAsync($"{baseAddress}api/FetchData/GetDetails/{title}/{value}");

        return res;
    }

    public async Task<List<RefDealerDto>> GetRefDealersAsync()
    {
        var dealerList = await _httpClient.GetFromJsonAsync<List<RefDealerDto>>($"{baseAddress}api/FetchData/GetDealers");
        return dealerList;
    }
}