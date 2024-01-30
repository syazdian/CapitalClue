using CapitalClue.Common.Models;

namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface ISyncData
{
    public Task<bool> SendUserId(string UserName);

    public Task StartSyncData();

    public Task UpdateChangesToServerDb();

    public Task UpdateLocalDbWithNewChangesFromServer();

    public Task<bool> InsertBellSources(CsvFileBellRecordsAnalysisDto recordsAnalysis);

    Task<bool> SendErrorLogs(List<ErrorLogDto> Errors);

    Task<bool> DeleteItems(ToDeleteItemsDto dto);
}