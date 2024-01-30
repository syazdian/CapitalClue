using CapitalClue.Common.Models.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface ILocalDbRepository
{
    Task GetTotalBellRecords();

    Task GetTotalStapleRecords();

    Task GetTotalTables();

    Task<IEnumerable<OnlyInBellDto>> GetBellSourceFromLocalDb(FilterItemDto filterItemDto);

    Task<IEnumerable<OnlyInStaplesDto>> GetStapleSourceFromLocalDb(FilterItemDto filterItemDto);

    Task<IEnumerable<CompareBellStapleDto>> GetBellStapleCompareFromLocalDb(FilterItemDto filterItemDto);

    Task GetStoreFromLocalDb();

    Task InsertSyncLog(SyncLogsDto syncLogsDto);

    Task InsertErrorLogs(ErrorLogDto errorLog);

    Task<List<ErrorLogDto>> GetAllErrorLogs();

    Task<bool> UpdateErrorLogs(List<ErrorLogDto> errorLogDtos);

    Task<ErrorLogDto?> GetErrorLogById(int id);

    Task<List<ErrorLogDto>?> GetErrorLogsNotUploaded();

    Task<List<BellSourceDto>> GetNotSyncedUpdatedBellSource();

    Task<List<StaplesSourceDto>> GetNotSyncedUpdatedStapleSource();

    Task UpdateLatestDownloadedBellAndStaplesToLocalDb(List<StaplesSourceDto> staples, List<BellSourceDto> bellSources);

    Task<long> InsertDataToLocalDbAsync(BellStaplesSourceDto bellStaplesSources);

    Task InsertBellSourceToLocalDbAsync(List<BellSourceDto> bellSourceDtos);

    Task InsertStoreSourceToLocalDbAsync(List<StoreDto> storeDto);

    Task InsertOnlyInBellToLocalDbAsync(List<OnlyInBellDto> onlyInBellDtos);

    Task InsertOnlyInStaplesToLocalDbAsync(List<OnlyInStaplesDto> onlyInStaplesDtos);

    Task InsertCompareBellStapleToLocalDbAsync(List<CompareBellStapleDto> compareBellStapleDtos);

    Task RemoveBellSourceFromLocalDbAsync(DateTime? startDate, DateTime? endDate);

    Task RemoveStapleSourceFromLocalDbAsync(DateTime? startDate, DateTime? endDate);

    Task RemoveOnlyInStaplesFromLocalDbAsync(DateTime? startDate, DateTime? endDate);

    Task RemoveOnlyInBellFromLocalDbAsync(DateTime? startDate, DateTime? endDate);

    Task RemoveInBothFromLocalDbAsync(DateTime? startDate, DateTime? endDate);

    Task InsertStaplesToLocalDbAsync(List<StaplesSourceDto> staplesSourceDtos);

    Task<bool> UpdateBellSourceList(List<BellSourceDto> bellSourceDtoList);

    Task<bool> UpdateStaplesSourceList(List<StaplesSourceDto> stapleSourceDtoList);

    Task<bool> UpdateBellSource(OnlyInBellDto bellSourceDto);

    Task<bool> UpdateStapleSource(OnlyInStaplesDto staplesSourceDto);

    Task<bool> UpdateCompareBellStaples(CompareBellStapleDto compareBellStapleDto);

    Task<bool> DeleteBellSource(long Id);

    Task<bool> DeleteStapleSource(long Id);

    Task<EntityEntry<BellSourceDto>> GetBellSourceEntry(BellSourceDto record);

    Task<bool> UpdateBellSource(long Id, string Comment, bool isReconciled);

    Task<bool> UpdateStapleSource(long Id, string Comment, bool isReconciled);

    Task<EntityEntry<StaplesSourceDto>> GetStapleSourceEntry(StaplesSourceDto record);

    Task<bool> LocalDbExist();

    Task<bool> PurgeTables();

    Task<DateTime> GetLatestSyncDate();

    Task LoadLocalDbToUi(FilterItemDto filterItemDto);

    Task InsertFetchHistory(DateTime? startDate = null, DateTime? endDate = null);

    Task<bool> GetOverlapDates(DateTime? startDate = null, DateTime? endDate = null);

    Task<List<FetchHistoryDto>> GetFetchHistory();

    Task<bool> RemoveFetchHistory(int Id);

    Task<bool> RemoveFetchHistory(DateTime? startDate, DateTime? endDate);

    Task SetSmallestAndLargestDate();
}