using Bell.Reconciliation.Common.Models;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Bell.Reconciliation.Frontend.Desktop.Services;

public class SyncData : ISyncData
{
    private readonly HttpClient _httpClient;
    private readonly ILocalDbRepository _localDb;
    private readonly string baseAddress;

    private readonly ILogger _logger;
    private DateTime latesSyncDate;
    private IStateContainer _stateContainer;

    public SyncData(HttpClient httpClient, ILocalDbRepository localDb, IStateContainer stateContainer, UrlKeeper url, ILogger<SyncData> logger)
    {
        _httpClient = httpClient;
        _localDb = localDb;
        _stateContainer = stateContainer;
        baseAddress = url.BaseUrl;
        _logger = logger;
    }

    public async Task StartSyncData()
    {
        await GetLatestSyncDate();
        await UpdateLocalDbWithNewChangesFromServer();
        await UpdateChangesToServerDb();
    }

    public async Task UpdateLocalDbWithNewChangesFromServer()
    {
        try
        {
            var formattedDateToSent = latesSyncDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var currentUser = _stateContainer.UserId ?? _stateContainer.UserName;
            var staplesList = await _httpClient.GetFromJsonAsync<List<StaplesSourceDto>>
                ($"{baseAddress}api/SyncData/GetLatestChangedStaplesSourceItemsByDate/{formattedDateToSent}/{currentUser}");
            var bellList = await _httpClient.GetFromJsonAsync<List<BellSourceDto>>
                ($"{baseAddress}api/SyncData/GetLatestChangedBellSourceItemsByDate/{formattedDateToSent}/{currentUser}");
            await _localDb.UpdateLatestDownloadedBellAndStaplesToLocalDb(staplesList, bellList);
        }
        catch (Exception ex)
        {
            _logger.LogError($"UpdateLocalDbWithNewChangesFromServer : {ex.Message}");

            throw;
        }
    }

    public async Task GetLatestSyncDate()
    {
        latesSyncDate = await _localDb.GetLatestSyncDate();
    }

    public async Task UpdateChangesToServerDb()
    {
        var notSyncedBellSources = await _localDb.GetNotSyncedUpdatedBellSource();
        var notSyncedStapleSources = await _localDb.GetNotSyncedUpdatedStapleSource();

        if (notSyncedBellSources.Count + notSyncedStapleSources.Count > 0)
        {
            SyncLogsDto syncLogsDto = new SyncLogsDto { StartSync = DateTime.UtcNow };
            try
            {
                if (notSyncedStapleSources.Count > 0)
                {
                    await _httpClient.PostAsJsonAsync($"{baseAddress}api/SyncData/SyncChangesStaple", notSyncedStapleSources);
                }

                if (notSyncedBellSources.Count > 0)
                {
                    await _httpClient.PostAsJsonAsync($"{baseAddress}api/SyncData/SyncChangesBell", notSyncedBellSources);
                }
                syncLogsDto.Success = true;

                if (syncLogsDto.Success == true)
                {
                    DateTime updateTime = syncLogsDto.StartSync;
                    notSyncedBellSources.ForEach(bell => bell.UpdateDate = updateTime);
                    notSyncedStapleSources.ForEach(staple => staple.UpdateDate = updateTime);
                    await _localDb.UpdateBellSourceList(notSyncedBellSources);
                    await _localDb.UpdateStaplesSourceList(notSyncedStapleSources);
                }
            }
            catch (Exception ex)
            {
                syncLogsDto.Success = false;
                _logger.LogError($"UpdateChangesToServerDb : {ex.Message}");

                throw;
            }
            finally
            {
                syncLogsDto.EndSync = DateTime.UtcNow;
                await _localDb.InsertSyncLog(syncLogsDto);
            }
        }
    }

    public async Task<bool> InsertBellSources(CsvFileBellRecordsAnalysisDto bellUpload)
    {
        try
        {
            CsvFileBellRecordsAnalysisDto dataToSend = new CsvFileBellRecordsAnalysisDto();
            dataToSend.FileName = bellUpload.FileName;
            dataToSend.UploadBy = bellUpload.UploadBy;
            dataToSend.SuccessRowCount = bellUpload.SuccessRowCount;
            dataToSend.FailRowCount = bellUpload.FailRowCount;
            dataToSend.IsFirstChunk = true;
            dataToSend.IsLastChunk = false;

            List<BellRecordAnalysis[]> uploadChunks = bellUpload.AnalysedRecords.Chunk(1000).ToList();
            int chunkCount = uploadChunks.Count;
            for (int i = 0; i < chunkCount; i++)
            {
                if (i == chunkCount - 1) dataToSend.IsLastChunk = true;

                dataToSend.AnalysedRecords = uploadChunks[i].ToList();

                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{baseAddress}api/SyncData/InsertBellSources", dataToSend);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody.Contains("success")) dataToSend.IsFirstChunk = false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"InsertBellSources : {ex.Message}");

            return false;
            throw;
        }
    }

    public async Task<bool> SendUserId(string UserName)
    {
        try
        {
            UserIdDto userId = new UserIdDto() { UserName = UserName };

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{baseAddress}api/Logs/SyncUserId", userId);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"SendUserId : {ex.Message}");

            return false;
            throw;
        }
    }

    public async Task<bool> SendErrorLogs(List<ErrorLogDto> Errors)
    {
        try
        {
            Errors.ForEach(error => error.UploadedDate = DateTime.UtcNow);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{baseAddress}api/Logs/InsertErrorLogs", Errors);
            if (response.IsSuccessStatusCode)
            {
                await _localDb.UpdateErrorLogs(Errors);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"SendErrorLogs : {ex.Message}");

            return false;
            throw;
        }
    }

    public async Task<bool> DeleteItems(ToDeleteItemsDto dto)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{baseAddress}api/SyncData/DeleteItems", dto);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"DeleteItems : {ex.Message}");

            return false;
            throw;
        }
    }
}