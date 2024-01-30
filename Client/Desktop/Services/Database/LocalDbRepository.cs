using Bell.Reconciliation.Common.Models;
using Bell.Reconciliation.Common.Models.Enums;
using Bell.Reconciliation.Frontend.Desktop.Data;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;

namespace Bell.Reconciliation.Frontend.Desktop.Services.Database;

public class LocalDbRepository : ILocalDbRepository
{
    private IStateContainer _stateContainer;
    private ILogger _logger;

    private List<BellSourceDto> totalBellRecords { get; set; } = new List<BellSourceDto>();
    private List<StaplesSourceDto> totalStaplesRecords { get; set; } = new List<StaplesSourceDto>();
    private List<OnlyInBellDto> totalOnlyInBell { get; set; } = new List<OnlyInBellDto>();
    private List<OnlyInStaplesDto> totalOnlyInstaples { get; set; } = new List<OnlyInStaplesDto>();
    private List<CompareBellStapleDto> totalCompareBellStaple { get; set; } = new List<CompareBellStapleDto>();

    public StapleSourceContext ctx { get; set; }

    public LocalDbRepository(StapleSourceContext context, IStateContainer stateContainer, ILogger<LocalDbRepository> logger)
    {
        ctx = context;
        _stateContainer = stateContainer;
        _logger = logger;
    }

    #region SYNC UPDATE DOWNLOAD LATEST CHANGES

    public async Task InsertSyncLog(SyncLogsDto syncLogsDto)
    {
        try
        {
            ctx.SyncLogs.Add(syncLogsDto);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"InsertSyncLog: {ex.Message}");

            throw;
        }
    }

    public async Task<DateTime> GetLatestSyncDate()
    {
        var recentSyncDate = ctx.SyncLogs.Where(x => x.Success == true).Max(c => c.EndSync);
        return (DateTime)recentSyncDate;
    }

    public async Task UpdateLatestDownloadedBellAndStaplesToLocalDb(List<StaplesSourceDto> staples, List<BellSourceDto> bellSources)
    {
        try
        {
            foreach (var bell in bellSources)
            {
                BellSourceDto? bellsourceInLocal = ctx.BellSources.Where(c => c.OrderNumber == bell.OrderNumber && c.RebateType == bell.RebateType).FirstOrDefault();
                if (bellsourceInLocal == null)
                {
                    ctx.BellSources.Add(bellsourceInLocal);
                    continue;
                }
                bellsourceInLocal.Comments = bell.Comments;
                bellsourceInLocal.MatchStatus = bell.MatchStatus;
                bellsourceInLocal.Reconciled = bell.Reconciled;
                bellsourceInLocal.ReconciledBy = bell.ReconciledBy;
                bellsourceInLocal.ReconciledDate = bell.ReconciledDate;
                bellsourceInLocal.UpdateDate = bell.UpdateDate;
                bellsourceInLocal.Imei = bell.Imei;
                bellsourceInLocal.Phone = bell.Phone;
                ctx.BellSources.Update(bellsourceInLocal);
            }

            foreach (var staple in staples)
            {
                StaplesSourceDto? staplesourceInLocal = ctx.StaplesSources.Where(c => c.OrderNumber == staple.OrderNumber && c.RebateType == staple.RebateType).FirstOrDefault();
                if (staplesourceInLocal == null)
                {
                    ctx.StaplesSources.Add(staplesourceInLocal);
                    continue;
                }
                staplesourceInLocal.Comments = staple.Comments;
                staplesourceInLocal.MatchStatus = staple.MatchStatus;
                staplesourceInLocal.Reconciled = staple.Reconciled;
                staplesourceInLocal.ReconciledBy = staple.ReconciledBy;
                staplesourceInLocal.ReconciledDate = staple.ReconciledDate;
                staplesourceInLocal.UpdateDate = staple.UpdateDate;
                staplesourceInLocal.Imei = staple.Imei;
                staplesourceInLocal.Phone = staple.Phone;
                ctx.StaplesSources.Update(staplesourceInLocal);
            }
            ctx.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError($"UpdateLatestDownloadedBellAndStaplesToLocalDb: {ex.Message}");

            throw;
        }
    }

    public async Task<List<BellSourceDto>> GetNotSyncedUpdatedBellSource()
    {
        try
        {
            var recentSyncDate = ctx.SyncLogs.Where(x => x.Success == true).Max(c => c.EndSync);
            var query = ctx.BellSources.Where(c => c.ReconciledDate.HasValue && c.ReconciledDate > recentSyncDate).AsQueryable();
            List<BellSourceDto> bellSourceDtos = query.ToList();
            return bellSourceDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetNotSyncedUpdatedBellSource: {ex.Message}");

            throw;
        }
    }

    public async Task<List<StaplesSourceDto>> GetNotSyncedUpdatedStapleSource()
    {
        try
        {
            var recentSyncDate = ctx.SyncLogs.Where(x => x.Success == true).Max(c => c.EndSync);
            var res = ctx.StaplesSources.Where(c => c.ReconciledDate.HasValue && c.ReconciledDate > recentSyncDate).ToList();
            List<StaplesSourceDto> staplesSourceDtos = res;
            return staplesSourceDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetNotSyncedUpdatedStapleSource: {ex.Message}");

            throw;
        }
    }

    #endregion SYNC UPDATE DOWNLOAD LATEST CHANGES

    #region Insert Or Delete Data to LocalDB

    public async Task InsertBellSourceToLocalDbAsync(List<BellSourceDto> bellSourceDtos)
    {
        try
        {
            totalBellRecords = totalBellRecords.Union(bellSourceDtos).ToList();

            await ctx.BellSources.AddRangeAsync(bellSourceDtos);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"InsertBellSourceToLocalDbAsync: {ex.Message}");

            throw;
        }
    }

    public async Task InsertStaplesToLocalDbAsync(List<StaplesSourceDto> staplesSourceDtos)
    {
        try
        {
            totalStaplesRecords = totalStaplesRecords.Union(staplesSourceDtos).ToList();

            await ctx.StaplesSources.AddRangeAsync(staplesSourceDtos);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"InsertStaplesToLocalDbAsync: {ex.Message}");

            throw;
        }
    }

    public async Task<long> InsertDataToLocalDbAsync(BellStaplesSourceDto bellStaplesSources)
    {
        try
        {
            await ctx.BellSources.AddRangeAsync(bellStaplesSources.BellSources.ToList());
            await ctx.StaplesSources.AddRangeAsync(bellStaplesSources.StaplesSources.ToList());
            await ctx.SaveChangesAsync();
            return bellStaplesSources.BellSources.Max(x => x.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"InsertDataToLocalDbAsync: {ex.Message}");

            throw;
        }
    }

    public async Task InsertStoreSourceToLocalDbAsync(List<StoreDto> storeDto)
    {
        try
        {
            await ctx.Store.AddRangeAsync(storeDto);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"InsertStoreSourceToLocalDbAsync: {ex.Message}");

            throw;
        }
    }

    public async Task RemoveBellSourceFromLocalDbAsync(DateTime? startDate, DateTime? endDate)
    {
        try
        {
            var items = ctx.BellSources.Where(c => (startDate == null || c.TransactionDate >= startDate) && (endDate == null || c.TransactionDate <= endDate)).ToList();
            ctx.BellSources.RemoveRange(items);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"RemoveBellSourceFromLocalDbAsync: {ex.Message}");

            throw;
        }
    }

    public async Task RemoveStapleSourceFromLocalDbAsync(DateTime? startDate, DateTime? endDate)
    {
        try
        {
            var items = ctx.StaplesSources.Where(c => (startDate == null || c.TransactionDate >= startDate) && (endDate == null || c.TransactionDate <= endDate)).ToList();
            ctx.StaplesSources.RemoveRange(items);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"RemoveStapleSourceFromLocalDbAsync: {ex.Message}");

            throw;
        }
    }

    public async Task RemoveOnlyInStaplesFromLocalDbAsync(DateTime? startDate, DateTime? endDate)
    {
        try
        {
            var items = ctx.OnlyInStaples.Where(c => (startDate == null || c.TransactionDate >= startDate) && (endDate == null || c.TransactionDate <= endDate)).ToList();
            ctx.OnlyInStaples.RemoveRange(items);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"RemoveOnlyInStaplesFromLocalDbAsync: {ex.Message}");

            throw;
        }
    }

    public async Task RemoveOnlyInBellFromLocalDbAsync(DateTime? startDate, DateTime? endDate)
    {
        try
        {
            var items = ctx.OnlyInBell.Where(c => (startDate == null || c.TransactionDate >= startDate) && (endDate == null || c.TransactionDate <= endDate)).ToList();
            ctx.OnlyInBell.RemoveRange(items);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"RemoveOnlyInBellFromLocalDbAsync: {ex.Message}");

            throw;
        }
    }

    public async Task RemoveInBothFromLocalDbAsync(DateTime? startDate, DateTime? endDate)
    {
        try
        {
            var items = ctx.CompareBellStaple.Where(c => (startDate == null || c.STransactionDate >= startDate || c.BTransactionDate >= startDate)
            && (endDate == null || c.STransactionDate <= endDate || c.BTransactionDate <= endDate)).ToList();
            ctx.CompareBellStaple.RemoveRange(items);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"RemoveInBothFromLocalDbAsync: {ex.Message}");

            throw;
        }
    }

    public async Task InsertOnlyInBellToLocalDbAsync(List<OnlyInBellDto> onlyInBellDtos)
    {
        try
        {
            await ctx.OnlyInBell.AddRangeAsync(onlyInBellDtos);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"InsertOnlyInBellToLocalDbAsync: {ex.Message}");

            throw;
        }
    }

    public async Task InsertOnlyInStaplesToLocalDbAsync(List<OnlyInStaplesDto> onlyInStaplesDtos)
    {
        try
        {
            await ctx.OnlyInStaples.AddRangeAsync(onlyInStaplesDtos);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"InsertOnlyInStaplesToLocalDbAsync: {ex.Message}");

            throw;
        }
    }

    public async Task InsertCompareBellStapleToLocalDbAsync(List<CompareBellStapleDto> compareBellStapleDtos)
    {
        try
        {
            await ctx.CompareBellStaple.AddRangeAsync(compareBellStapleDtos);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"InsertOnlyInStaplesToLocalDbAsync: {ex.Message}");

            throw;
        }
    }

    #endregion Insert Or Delete Data to LocalDB

    #region GET DATA FROM LOCALDB TO MEMORY

    public async Task GetTotalBellRecords()
    {
        totalBellRecords = await ctx.BellSources.ToListAsync();

        totalBellRecords = totalBellRecords.GroupBy(t => new { t.Phone, t.RebateType, t.TransactionDate, t.OrderNumber, t.Lob, t.SubLob })
            .Select(g => new BellSourceDto
            {
                Amount = g.Sum(c => c.Amount),
                Brand = g.First().Brand,
                Comments = g.First().Comments,
                CreateDate = g.First().CreateDate,
                CustomerAccount = g.First().CustomerAccount,
                CustomerName = g.First().CustomerName,
                DealerCode = g.First().DealerCode,
                Id = g.First().Id,
                Imei = g.First().Imei,
                Lob = g.First().Lob,
                MatchStatus = g.First().MatchStatus,
                OrderNumber = g.First().OrderNumber,
                Phone = g.First().Phone,
                Product = g.First().Product,
                RebateType = g.First().RebateType,
                Reconciled = g.First().Reconciled,
                ReconciledBy = g.First().ReconciledBy,
                ReconciledDate = g.First().ReconciledDate,
                Simserial = g.First().Simserial,
                StoreNumber = g.First().StoreNumber,
                SubLob = g.First().SubLob,
                TransactionDate = g.First().TransactionDate,
                UpdateDate = g.First().UpdateDate,
                UpdatedBy = g.First().UpdatedBy,
            }
            ).ToList();

        return;
    }

    public async Task GetTotalStapleRecords()
    {
        totalStaplesRecords = await ctx.StaplesSources.ToListAsync();

        return;
    }

    public async Task GetTotalTables()
    {
        try
        {
            if (!totalBellRecords.Any())
                await GetTotalBellRecords();
            if (!totalStaplesRecords.Any())
                await GetTotalStapleRecords();

            var compareBellStapleCellPhoneByPhone = (from b in totalBellRecords.Where(c => c.RebateType != null && c.Phone != null).ToList()
                                                     join s in totalStaplesRecords.Where(c => c.RebateType != null && c.Phone != null).ToList()
                                                     on new { phone = b.Phone, rebateType = b.RebateType } equals new { phone = s.Phone, rebateType = s.RebateType }
                                                     where (b.Lob?.ToLower() == "wireless" && s.Lob?.ToLower() == "wireless")
                                                     select new CompareBellStapleDto
                                                     {
                                                         BPhone = b.Phone,
                                                         BLob = b.Lob,
                                                         BSubLob = b.SubLob,
                                                         BImei = b.Imei,
                                                         BOrderNumber = b.OrderNumber,
                                                         BAmount = b.Amount,
                                                         BComments = b.Comments,
                                                         BTransactionDate = b.TransactionDate,
                                                         BCustomerName = b.CustomerName,
                                                         BRebateType = b.RebateType,
                                                         BReconciled = b.Reconciled,
                                                         BUpdateDate = b.UpdateDate,
                                                         BReconciledBy = b.ReconciledBy,
                                                         BReconciledDate = b.ReconciledDate,
                                                         BProduct = b.Product,
                                                         BBrand = b.Brand,
                                                         BId = b.Id,
                                                         BStoreNumber = b.StoreNumber,

                                                         SPhone = s.Phone,
                                                         SLob = s.Lob,
                                                         SSubLob = s.SubLob,
                                                         SImei = s.Imei,
                                                         SOrderNumber = s.OrderNumber,
                                                         SAmount = s.Amount,
                                                         SComments = s.Comments,
                                                         STransactionDate = s.TransactionDate,
                                                         SCustomerName = s.CustomerName,
                                                         SRebateType = s.RebateType,
                                                         SReconciled = s.Reconciled,
                                                         SUpdateDate = s.UpdateDate,
                                                         SReconciledBy = s.ReconciledBy,
                                                         SReconciledDate = s.ReconciledDate,
                                                         SProduct = s.Product,
                                                         SBrand = s.Brand,
                                                         SLocation = s.Location,
                                                         SId = s.Id,
                                                         SBellTransactionId = s.BellTransactionId,
                                                         SStoreNumber = s.StoreNumber,

                                                         IsReconciled = ((s.Reconciled == b.Reconciled) && s.Reconciled != null) ? (bool)s.Reconciled : false,

                                                         MatchStatus = (((short?)s.MatchStatus == (short?)b.MatchStatus) && (short?)s.MatchStatus != null)
                                                         ? (MatchStatus)s.MatchStatus :
                                                         ((s.Amount == b.Amount && s.Phone == b.Phone && s.RebateType == b.RebateType)
                                                         ? MatchStatus.Match : MatchStatus.Mismatch)
                                                     }).ToList();

            var compareBellStapleNonCellPhone = (from b in totalBellRecords.Where(c => c.OrderNumber != null && c.SubLob != null).ToList()
                                                 join s in totalStaplesRecords.Where(c => c.OrderNumber != null && c.SubLob != null).ToList()
                                                 on new { ordernumber = b.OrderNumber, sublob = b.SubLob } equals new { ordernumber = s.OrderNumber, sublob = s.SubLob }
                                                 where b.Lob?.ToLower() == "wireline" && s.Lob?.ToLower() == "wireline"
                                                 select new CompareBellStapleDto
                                                 {
                                                     BPhone = b.Phone,
                                                     BLob = b.Lob,
                                                     BSubLob = b.SubLob,
                                                     BImei = b.Imei,
                                                     BOrderNumber = b.OrderNumber,
                                                     BAmount = b.Amount,
                                                     BComments = b.Comments,
                                                     BTransactionDate = b.TransactionDate,
                                                     BCustomerName = b.CustomerName,
                                                     BRebateType = b.RebateType,
                                                     BReconciled = b.Reconciled,
                                                     BUpdateDate = b.UpdateDate,
                                                     BReconciledBy = b.ReconciledBy,
                                                     BReconciledDate = b.ReconciledDate,
                                                     BProduct = b.Product,
                                                     BBrand = b.Brand,
                                                     BId = b.Id,
                                                     BStoreNumber = b.StoreNumber,

                                                     SPhone = s.Phone,
                                                     SLob = s.Lob,
                                                     SSubLob = s.SubLob,
                                                     SImei = s.Imei,
                                                     SOrderNumber = s.OrderNumber,
                                                     SAmount = s.Amount,
                                                     SComments = s.Comments,
                                                     STransactionDate = s.TransactionDate,
                                                     SCustomerName = s.CustomerName,
                                                     SRebateType = s.RebateType,
                                                     SReconciled = s.Reconciled,
                                                     SUpdateDate = s.UpdateDate,
                                                     SReconciledBy = s.ReconciledBy,
                                                     SReconciledDate = s.ReconciledDate,
                                                     SProduct = s.Product,
                                                     SBrand = s.Brand,
                                                     SLocation = s.Location,
                                                     SId = s.Id,
                                                     SBellTransactionId = s.BellTransactionId,
                                                     SStoreNumber = s.StoreNumber,

                                                     IsReconciled = ((s.Reconciled == b.Reconciled) && s.Reconciled != null) ? (bool)s.Reconciled : false,

                                                     MatchStatus = (((short?)s.MatchStatus == (short?)b.MatchStatus) && (short?)s.MatchStatus != null)
                                                     ? (MatchStatus)s.MatchStatus :
                                                     ((s.Amount == b.Amount && s.OrderNumber == b.OrderNumber && s.SubLob == b.SubLob)
                                                     ? MatchStatus.Match : MatchStatus.Mismatch)
                                                 });

            totalCompareBellStaple = compareBellStapleCellPhoneByPhone.Union(compareBellStapleNonCellPhone).ToList();
            await InsertCompareBellStapleToLocalDbAsync(totalCompareBellStaple);

            var bellQuery = (from b in totalBellRecords
                             join c in totalCompareBellStaple on b.Id equals c.BId into comparejointable
                             from j in comparejointable.DefaultIfEmpty()
                             where j == null
                             select b).ToList();
            totalOnlyInBell = bellQuery.Adapt<List<OnlyInBellDto>>();
            await InsertOnlyInBellToLocalDbAsync(totalOnlyInBell);

            var stapleQuery = (from b in totalStaplesRecords
                               join c in totalCompareBellStaple on b.Id equals c.SId into comparejointable
                               from j in comparejointable.DefaultIfEmpty()
                               where j == null
                               select b).ToList();
            totalOnlyInstaples = stapleQuery.Adapt<List<OnlyInStaplesDto>>();
            await InsertOnlyInStaplesToLocalDbAsync(totalOnlyInstaples);

            totalBellRecords.Clear();
            totalStaplesRecords.Clear();

            await GetStoreFromLocalDb();

            return;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetTotalTables: {ex.Message}");

            return;
        }
    }

    #endregion GET DATA FROM LOCALDB TO MEMORY

    #region QUERY FOR UI DATAGRIDS

    public async Task<IEnumerable<OnlyInBellDto>> GetBellSourceFromLocalDb(FilterItemDto filterItemDto)
    {
        var onlyInBellRecords = from b in ctx.OnlyInBell
                                select b;

        if (!string.IsNullOrEmpty(filterItemDto.Brand))
            onlyInBellRecords = onlyInBellRecords.Where(b => b.Brand.ToLower() == filterItemDto.Brand.ToLower());
        if (!string.IsNullOrEmpty(filterItemDto.StoreNumber))
            onlyInBellRecords = onlyInBellRecords.Where(b => b.StoreNumber == filterItemDto.StoreNumber);
        if (!string.IsNullOrEmpty(filterItemDto.RebateValue))
            onlyInBellRecords = onlyInBellRecords.Where(b => b.RebateType == filterItemDto.RebateValue);
        if (!string.IsNullOrEmpty(filterItemDto.Lob))
            onlyInBellRecords = onlyInBellRecords.Where(b => b.Lob == filterItemDto.Lob);
        if (!string.IsNullOrEmpty(filterItemDto.SubLob))
            onlyInBellRecords = onlyInBellRecords.Where(b => b.SubLob == filterItemDto.SubLob);

        return onlyInBellRecords;
    }

    public async Task<IEnumerable<OnlyInStaplesDto>> GetStapleSourceFromLocalDb(FilterItemDto filterItemDto)
    {
        var onlyInStaplesRecords = from s in ctx.OnlyInStaples
                                   select s;

        if (!string.IsNullOrEmpty(filterItemDto.Brand))
            onlyInStaplesRecords = onlyInStaplesRecords.Where(s => s.Brand.ToLower() == filterItemDto.Brand.ToLower());
        if (!string.IsNullOrEmpty(filterItemDto.StoreNumber))
            onlyInStaplesRecords = onlyInStaplesRecords.Where(s => s.StoreNumber == filterItemDto.StoreNumber);
        if (!string.IsNullOrEmpty(filterItemDto.RebateValue))
            onlyInStaplesRecords = onlyInStaplesRecords.Where(s => s.RebateType == filterItemDto.RebateValue);
        if (!string.IsNullOrEmpty(filterItemDto.Lob))
            onlyInStaplesRecords = onlyInStaplesRecords.Where(s => s.Lob == filterItemDto.Lob);
        if (!string.IsNullOrEmpty(filterItemDto.SubLob))
            onlyInStaplesRecords = onlyInStaplesRecords.Where(s => s.SubLob == filterItemDto.SubLob);

        return onlyInStaplesRecords;
    }

    public async Task<IEnumerable<CompareBellStapleDto>> GetBellStapleCompareFromLocalDb(FilterItemDto filterItemDto)
    {
        try
        {
            var compareBellStapleRecords = from sb in ctx.CompareBellStaple
                                           select sb;

            if (!string.IsNullOrEmpty(filterItemDto.Brand))
                compareBellStapleRecords = compareBellStapleRecords.Where(sb => sb.SBrand.ToLower() == filterItemDto.Brand.ToLower() && sb.BBrand.ToLower() == filterItemDto.Brand.ToLower());
            if (!string.IsNullOrEmpty(filterItemDto.StoreNumber))
                compareBellStapleRecords = compareBellStapleRecords.Where(sb => sb.SStoreNumber == filterItemDto.StoreNumber && sb.BStoreNumber == filterItemDto.StoreNumber);
            if (!string.IsNullOrEmpty(filterItemDto.RebateValue))
                compareBellStapleRecords = compareBellStapleRecords.Where(sb => sb.SRebateType == filterItemDto.RebateValue && sb.BRebateType == filterItemDto.RebateValue);
            if (!string.IsNullOrEmpty(filterItemDto.Lob))
                compareBellStapleRecords = compareBellStapleRecords.Where(sb => sb.SLob == filterItemDto.Lob && sb.BLob == filterItemDto.Lob);
            if (!string.IsNullOrEmpty(filterItemDto.SubLob))
                compareBellStapleRecords = compareBellStapleRecords.Where(sb => sb.SSubLob == filterItemDto.SubLob && sb.BSubLob == filterItemDto.SubLob);

            return compareBellStapleRecords;
        }
        catch (Exception ex)
        {
            //s_logger.LogError($"GetBellStapleCompareFromLocalDb: {ex.Message}");
            return null;
        }
    }

    public async Task GetStoreFromLocalDb()
    {
        if (_stateContainer.StoreNumberDisplay.Any())
        {
            return;
        }

        List<string?>? store = new();
        var bellStrores = totalOnlyInBell.Select(x => x.StoreNumber)?.Where(x => !string.IsNullOrEmpty(x))?.ToList() ?? new();
        var stapleStores = totalOnlyInstaples.Select(x => x.StoreNumber)?.Where(x => !string.IsNullOrEmpty(x))?.ToList() ?? new();
        var compareStores = totalCompareBellStaple.SelectMany(q => new[] { q.BStoreNumber, q.SStoreNumber }).Where(s => !string.IsNullOrEmpty(s)).ToList() ?? new();

        if (stapleStores.Any()) store.AddRange(stapleStores);
        if (bellStrores.Any()) store.AddRange(bellStrores);
        if (compareStores.Any()) store.AddRange(compareStores);
        if (store.Any())
            _stateContainer.StoreNumberDisplay = store?.OrderBy(x => int.TryParse(x, out int v) ? v : 0).Distinct().ToList();

        return;
    }

    public async Task SetSmallestAndLargestDate()
    {
        if (_stateContainer.SmallestDateLocalDb != null)
        {
            return;
        }
        List<DateTime> minDates = new();
        DateTime? minBellDate = totalOnlyInBell.Min(x => x.TransactionDate);
        if (minBellDate != null && minBellDate != DateTime.MinValue) minDates.Add((DateTime)minBellDate);

        var minStaplesDate = totalOnlyInstaples.Min(x => x.TransactionDate);
        if (minStaplesDate != null && minStaplesDate != DateTime.MinValue) minDates.Add((DateTime)minStaplesDate);

        var minCompareDate = totalCompareBellStaple.SelectMany(q => new[] { q.BTransactionDate, q.STransactionDate }).Min();
        if (minCompareDate != null && minCompareDate != DateTime.MinValue) minDates.Add((DateTime)minCompareDate);

        DateTime? minDate = minDates.Min();

        List<DateTime> maxDates = new();
        var maxBellDate = totalOnlyInBell.Max(x => x.TransactionDate);
        if (maxBellDate != null && maxBellDate != DateTime.MinValue) maxDates.Add((DateTime)maxBellDate);

        var maxStaplesDate = totalOnlyInstaples.Max(x => x.TransactionDate);
        if (maxStaplesDate != null && maxStaplesDate != DateTime.MinValue) maxDates.Add((DateTime)maxStaplesDate);

        var maxCompareDate = totalCompareBellStaple.SelectMany(q => new[] { q.BTransactionDate, q.STransactionDate }).Max();
        if (maxCompareDate != null && maxCompareDate != DateTime.MinValue) maxDates.Add((DateTime)maxCompareDate);

        DateTime? maxDate = maxDates.Max();

        _stateContainer.SmallestDateLocalDb = minDate;
        _stateContainer.LatestDateLocalDb = maxDate;

        return;
    }

    #endregion QUERY FOR UI DATAGRIDS

    #region Update LocalDB while Reconciling

    public async Task<bool> UpdateBellSourceList(List<BellSourceDto> bellSourceDtoList)
    {
        try
        {
            foreach (var bellSource in bellSourceDtoList)
            {
                ctx.Update(bellSource);
                ctx.SaveChanges();
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"UpdateBellSourceList: {ex.Message}");

            return false;
        }
    }

    public async Task<bool> UpdateStaplesSourceList(List<StaplesSourceDto> stapleSourceDtoList)
    {
        try
        {
            foreach (var staples in stapleSourceDtoList)
            {
                ctx.Update(staples);
                ctx.SaveChanges();
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"UpdateStaplesSourceList: {ex.Message}");

            return false;
        }
    }

    public async Task<bool> UpdateBellSource(OnlyInBellDto bellSourceDto)
    {
        try
        {
            bellSourceDto.ReconciledBy = _stateContainer.UserId ?? "USER";
            bellSourceDto.ReconciledDate = DateTime.UtcNow;

            ctx.Update(bellSourceDto);
            ctx.SaveChanges();

            await UpdateBellSource(bellSourceDto.Id, bellSourceDto.Comments, bellSourceDto.Reconciled);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"UpdateBellSource: {ex.Message}");

            return false;
        }
    }

    public async Task<bool> UpdateBellSource(long Id, string Comment, bool isReconciled)
    {
        try
        {
            var bell = ctx.BellSources.Single(c => c.Id == Id);
            if (bell is not null)
            {
                bell.Reconciled = isReconciled;
                bell.Comments = Comment;
                // bell.MatchStatus = (short)matchStatus;
                bell.ReconciledBy = _stateContainer.UserId ?? "USER";
                bell.ReconciledDate = DateTime.UtcNow;
                ctx.Update(bell);
                ctx.SaveChanges();
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"UpdateBellSource: {ex.Message}");

            return false;
        }
    }

    public async Task<bool> UpdateStapleSource(OnlyInStaplesDto staplesSourceDto)
    {
        try
        {
            staplesSourceDto.ReconciledBy = _stateContainer.UserId ?? "USER";
            staplesSourceDto.ReconciledDate = DateTime.UtcNow;

            ctx.Update(staplesSourceDto);
            ctx.SaveChanges();

            await UpdateStapleSource(staplesSourceDto.Id, staplesSourceDto.Comments, staplesSourceDto.Reconciled);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetErrorLogById: {ex.Message}");

            return false;
        }
    }

    public async Task<bool> UpdateStapleSource(long Id, string Comment, bool isReconciled)
    {
        try
        {
            var staple = ctx.StaplesSources.Single(c => c.Id == Id);

            if (staple is not null)
            {
                staple.Reconciled = isReconciled;
                staple.Comments = Comment;
                //staple.MatchStatus = (short)matchStatus;
                staple.ReconciledBy = _stateContainer.UserId ?? "USER";
                staple.ReconciledDate = DateTime.UtcNow;
                ctx.Update(staple);
                ctx.SaveChanges();
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"UpdateStapleSource: {ex.Message}");

            return false;
        }
    }

    public async Task<bool> UpdateCompareBellStaples(CompareBellStapleDto compareBellStapleDto)
    {
        try
        {
            compareBellStapleDto.SReconciledBy = _stateContainer.UserId ?? "USER";
            compareBellStapleDto.BReconciledBy = _stateContainer.UserId ?? "USER";
            compareBellStapleDto.BReconciledDate = DateTime.UtcNow;
            compareBellStapleDto.SReconciledDate = DateTime.UtcNow;

            ctx.Update(compareBellStapleDto);
            ctx.SaveChanges();

            await UpdateStapleSource(compareBellStapleDto.SId, compareBellStapleDto.SComments, compareBellStapleDto.IsReconciled);
            await UpdateBellSource(compareBellStapleDto.BId, compareBellStapleDto.BComments, compareBellStapleDto.IsReconciled);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"UpdateCompareBellStaples: {ex.Message}");

            return false;
        }
    }

    #endregion Update LocalDB while Reconciling

    #region Delete LocalDb

    public async Task<bool> DeleteBellSource(long Id)
    {
        try
        {
            var bell = ctx.BellSources.Single(c => c.Id == Id);

            if (bell is not null)
            {
                ctx.Remove(bell);
                ctx.SaveChanges();
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"DeleteBelleSource: {ex.Message}");

            return false;
        }
    }

    public async Task<bool> DeleteStapleSource(long Id)
    {
        try
        {
            var staple = ctx.StaplesSources.Single(c => c.Id == Id);

            if (staple is not null)
            {
                ctx.Remove(staple);
                ctx.SaveChanges();
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"DeleteStapleSource: {ex.Message}");

            return false;
        }
    }

    #endregion Delete LocalDb

    #region Load LocalDB to UI DataGrid

    public async Task TablesExistInMemory()
    {
        //if (!totalOnlyInBell.Any() || !totalOnlyInstaples.Any() || !totalCompareBellStaple.Any())
        await GetTotalTables();
    }

    public async Task LoadLocalDbToUi(FilterItemDto filterItemDto)
    {
        //await TablesExistInMemory();
        //await GetTotalTables();

        _stateContainer.compareBellStaple = null;
        _stateContainer.compareBellStaple = await GetBellStapleCompareFromLocalDb(filterItemDto);
        _stateContainer.bellSources = null;
        _stateContainer.bellSources = await GetBellSourceFromLocalDb(filterItemDto);
        _stateContainer.staplesSources = null;
        _stateContainer.staplesSources = await GetStapleSourceFromLocalDb(filterItemDto);
    }

    public async Task<EntityEntry<BellSourceDto>> GetBellSourceEntry(BellSourceDto record)
    {
        var entityEntry = ctx.Entry(record);

        return entityEntry;
    }

    public async Task<EntityEntry<StaplesSourceDto>> GetStapleSourceEntry(StaplesSourceDto record)
    {
        var entityEntry = ctx.Entry(record);

        return entityEntry;
    }

    public async Task<bool> LocalDbExist()
    {
        try
        {
            if (ctx.BellSources.Count() > 1 || ctx.StaplesSources.Count() > 1)
            {
                return true; ;
            }
            return false;
        }
        catch (Exception ex)
        {
            await ctx.Database.EnsureCreatedAsync();
            _logger.LogError($"LocalDbExist: {ex.Message}");

            return false;
        }
    }

    public async Task<bool> PurgeTables()
    {
        try
        {
            await ctx.BellSources?.ExecuteDeleteAsync();
            await Task.Delay(1);
            await ctx.StaplesSources?.ExecuteDeleteAsync();
            await Task.Delay(1);
            await ctx.SyncLogs?.ExecuteDeleteAsync();
            await Task.Delay(1);

            _stateContainer.bellSources = null;
            _stateContainer.staplesSources = null;
            _stateContainer.compareBellStaple = null;

            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"PurgeTables: {ex.Message}");

            throw;
        }
    }

    #endregion Load LocalDB to UI DataGrid

    #region ErrorLogs

    public async Task InsertErrorLogs(ErrorLogDto errorLog)
    {
        try
        {
            ctx.ErrorLogs.Add(errorLog);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"InsertErrorLogs: {ex.Message}");

            throw;
        }
    }

    public async Task<List<ErrorLogDto>> GetErrorLogsNotUploaded()
    {
        try
        {
            List<ErrorLogDto> res = ctx.ErrorLogs.Where(X => X.UploadedDate == DateTime.MinValue).AsQueryable().ToList();
            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetErrorLogsNotUploaded: {ex.Message}");

            throw;
        }
    }

    public async Task<List<ErrorLogDto>> GetAllErrorLogs()
    {
        try
        {
            List<ErrorLogDto> res = ctx.ErrorLogs.AsQueryable().OrderByDescending(x => x.CreatedDate).ToList();
            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetAllErrorLogs: {ex.Message}");

            throw;
        }
    }

    public async Task<ErrorLogDto?> GetErrorLogById(int id)
    {
        try
        {
            ErrorLogDto? res = ctx.ErrorLogs.Where(x => x.Id == id)?.FirstOrDefault();
            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetErrorLogById: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> UpdateErrorLogs(List<ErrorLogDto> errorLogDtos)
    {
        try
        {
            ctx.UpdateRange(errorLogDtos);
            ctx.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"UpdateErrorLogs: {ex.Message}");
            throw;
        }
    }

    #endregion ErrorLogs

    #region FetchHistory

    public async Task InsertFetchHistory(DateTime? startDate = null, DateTime? endDate = null)
    {
        try
        {
            SetSmallestAndLargestDate();
            FetchHistoryDto fetchHistory = new FetchHistoryDto
            {
                UserId = "USER", //TODO
                StartDate = startDate.HasValue ? startDate.Value : _stateContainer.SmallestDateLocalDb,
                EndDate = endDate.HasValue ? endDate.Value : _stateContainer.LatestDateLocalDb,
                DateCreate = DateTime.Now
            };

            ctx.FetchHistory.Add(fetchHistory);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"InsertFecthHistory: {ex.Message}");

            throw;
        }
    }

    public async Task<bool> GetOverlapDates(DateTime? startDate = null, DateTime? endDate = null)
    {
        try
        {
            if (!endDate.HasValue)
                endDate = DateTime.Now;
            if (!startDate.HasValue)
                startDate = DateTime.MinValue;

            var quety = ctx.FetchHistory.Where(c =>
            (c.StartDate <= startDate && c.EndDate >= endDate) || (c.StartDate >= startDate && c.EndDate <= endDate) ||
            (c.StartDate <= startDate && startDate <= c.EndDate && c.EndDate <= endDate) ||
             (startDate <= c.StartDate && c.StartDate <= endDate && endDate <= c.EndDate)
            ).ToList();

            if (quety.Count() > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetOverlapDates: {ex.Message}");

            throw;
        }
    }

    public async Task<List<FetchHistoryDto>> GetFetchHistory()
    {
        try
        {
            var query = ctx.FetchHistory.OrderBy(c => c.StartDate).ToList();
            return query;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetFetchHistory: {ex.Message}");

            throw;
        }
    }

    public async Task<bool> RemoveFetchHistory(DateTime? startDate, DateTime? endDate)
    {
        try
        {
            var fetchHistoryItemsToRemove = ctx.FetchHistory.Where(item => item.StartDate >= startDate && item.EndDate <= endDate).ToList();

            foreach (var item in fetchHistoryItemsToRemove)
            {
                var query = ctx.FetchHistory.Remove(item);
                await ctx.SaveChangesAsync();
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetFetchHistory: {ex.Message}");
            return false;

            throw;
        }
    }

    public async Task<bool> RemoveFetchHistory(int Id)
    {
        try
        {
            var fetchHistory = ctx.FetchHistory.FirstOrDefault(c => c.Id == Id);

            if (fetchHistory != null)
            {
                var query = ctx.FetchHistory.Remove(fetchHistory);
                await ctx.SaveChangesAsync();

                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetFetchHistory: {ex.Message}");

            throw;
        }
    }

    Task<long> ILocalDbRepository.InsertDataToLocalDbAsync(BellStaplesSourceDto bellStaplesSources)
    {
        throw new NotImplementedException();
    }

    #endregion FetchHistory

    //public async Task<List<CompareBellStapleNonCellPhone>> GetBellStapleCompareNonCellPhoneFromLocalDb()
    //{
    //    //FormattableString query = $"SELECT stp.Amount as SAmount, stp.Phone as SPhone, bll.Amount as BAmount, bll.Phone as BPhone FROM \"BellSource\" as bll\r\njoin \"StaplesSource\" as stp on bll.Id = stp.id where bll.SubLob = 'Wireless' and stp.SubLob = 'Wireless' ";
    //    //var bellStaplesCompres = ctx.Database.SqlQuery<CompareBellStapleCellPhoneDto>(query).ToList();

    //    var query = from b in ctx.BellSources
    //                join s in ctx.StaplesSources on b.Id equals s.Id
    //                where s.SubLob != "Wireless" && b.SubLob != "Wireless"
    //                select new CompareBellStapleNonCellPhone
    //                {
    //                    BOrderNumber = b.OrderNumber.ToString(),
    //                    BAmount = b.Amount,
    //                    BComment = b.Comment.ToString(),
    //                    BTransactionDate = b.TransactionDate.ToString(),
    //                    BCustomerName = b.CustomerName.ToString(),
    //                    BRebateType = b.RebateType.ToString(),
    //                    BReconciled = b.Reconciled,

    //                    SOrderNumber = s.OrderNumber.ToString(),
    //                    SAmount = s.Amount,
    //                    SComment = s.Comment.ToString(),
    //                    STransactionDate = s.TransactionDate.ToString(),
    //                    SCustomerName = s.CustomerName.ToString(),
    //                    SRebateType = s.RebateType.ToString(),
    //                    SReconciled = s.Reconciled,
    //                    MatchStatus = (s.Reconciled == true && b.Reconciled == true) ? MatchStatus.Reconciled :
    //                    ((s.Amount == b.Amount && s.OrderNumber == b.OrderNumber &&
    //                    s.TransactionDate == b.TransactionDate && s.CustomerName == b.CustomerName && s.Imei == b.Imei && s.Phone == s.Phone) ? MatchStatus.Match : MatchStatus.Missmatch)
    //                };
    //    var bellStaplesCompres = query.ToList();

    //    return bellStaplesCompres;
    //}
}