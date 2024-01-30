using CapitalClue.Common.Models;
using CapitalClue.Common.Utilities;
using CapitalClue.Web.Server.Data.Sqlserver;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Xml;

namespace CapitalClue.Web.Server.Services
{
    public class ServerDbRepository
    {
        private readonly BellRecContext _bellDbContext;
        private readonly ILogger _logger;

        public ServerDbRepository(BellRecContext bellContext, ILogger<ServerDbRepository> logger)
        {
            _bellDbContext = bellContext;
            _logger = logger;
        }

        #region Fetch Data From Server for LocalDB

        public async Task<BellStaplesSourceDto> FetchFromDatabaseBellStaplesSource()
        {
            try
            {
                var bellSourcesDb = _bellDbContext.ReconBells.ToList();
                List<BellSourceDto> bellSources = bellSourcesDb.Adapt<List<BellSourceDto>>();

                var stapleSourcesDb = await _bellDbContext.ReconStaples.ToListAsync();
                List<StaplesSourceDto> stapleSources = stapleSourcesDb.Adapt<List<StaplesSourceDto>>();

                BellStaplesSourceDto bellstaple = new();
                bellstaple.BellSources = bellSources.ToArray();
                bellstaple.StaplesSources = stapleSources.ToArray();

                return bellstaple;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in FetchFromDatabaseBellStaplesSource: {ex.Message}");
                throw;
            }
        }

        public async Task<BellStapleCountDto> CountBellStapleRows(DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            try
            {
                BellStapleCountDto bellStapleCountDto = new BellStapleCountDto();

                bellStapleCountDto.StaplesCount = await _bellDbContext.ReconStaples.Where(c =>
                    (!dateFrom.HasValue || c.TransactionDate >= dateFrom.Value) &&
                    (!dateTo.HasValue || c.TransactionDate <= dateTo.Value)
                    ).CountAsync();

                bellStapleCountDto.BellCount = await _bellDbContext.ReconBells.Where(c =>
                    (!dateFrom.HasValue || c.TransactionDate >= dateFrom.Value) &&
                    (!dateTo.HasValue || c.TransactionDate <= dateTo.Value)
                    ).CountAsync();

                return bellStapleCountDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in CountBellStapleRows: {ex.Message}");
                throw;
            }
        }

        public async Task<List<BellSourceDto>> GetBellSource(DateTime? startDate = null, DateTime? endDate = null, int startCount = 1, int endCount = 1)
        {
            try
            {
                var items = await _bellDbContext.ReconBells.Where(c =>
                    (!startDate.HasValue || c.TransactionDate >= startDate.Value) &&
                    (!endDate.HasValue || c.TransactionDate <= endDate.Value)
                    ).OrderBy(e => e.Id).Skip(startCount - 1).Take(endCount - startCount + 1).ToListAsync();
                var adapted = items.Adapt<List<BellSourceDto>>();
                return adapted;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in CountBellStapleRows: {ex.Message}");
                throw;
            }
        }

        public async Task<List<StaplesSourceDto>> GetStaplesSource(DateTime? startDate = null, DateTime? endDate = null, int startCount = 1, int endCount = 1)
        {
            try
            {
                var items = await _bellDbContext.ReconStaples.Where(c =>
                    (!startDate.HasValue || c.TransactionDate >= startDate.Value) &&
                    (!endDate.HasValue || c.TransactionDate <= endDate.Value)
                    ).OrderBy(e => e.Id).Skip(startCount - 1).Take(endCount - startCount + 1).ToListAsync();

                var adapted = items.Adapt<List<StaplesSourceDto>>();
                return adapted;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in GetStaplesSource: {ex.Message}");
                throw;
            }
        }

        public async Task<List<StoreDto>> GetStores()
        {
            try
            {
                TypeAdapterConfig<RefStore, StoreDto>.NewConfig().Map(dest => dest.Id, src => src.StoreId);
                var items = await _bellDbContext.RefStores.ToListAsync();

                var adapted = items.Adapt<List<StoreDto>>();
                return adapted;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in GetStores: {ex.Message}");
                throw;
            }
        }

        public async Task<List<RefDealerDto>> GetDealers()
        {
            try
            {
                //TypeAdapterConfig<RefStore, RefDealerDto>.NewConfig().Map(dest => dest.Id, src => src.StoreId);
                var items = await _bellDbContext.RefDealers.ToListAsync();

                var adapted = items.Adapt<List<RefDealerDto>>();
                return adapted;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in GetDealers: {ex.Message}");
                throw;
            }
        }

        #endregion Fetch Data From Server for LocalDB

        #region Sync Data From LocalDB to Server

        public async Task<List<BellSourceDto>> GetBellSourceByLatestReconciledDate(DateTime dateTime, string user)
        {
            try
            {
                var items = await _bellDbContext.ReconBells.Where(x => x.ReconciledDate >= dateTime && x.ReconciledBy != user).ToListAsync();
                var adapted = items.Adapt<List<BellSourceDto>>();
                return adapted;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in GetBellSourceByLatestReconciledDate: {ex.Message}");
                throw;
            }
        }

        public async Task<List<StaplesSourceDto>> GetStaplesSourceLatestReconciledDate(DateTime dateTime, string user)
        {
            try
            {
                var items = await _bellDbContext.ReconStaples.Where(x => x.ReconciledDate >= dateTime && x.ReconciledBy != user).ToListAsync(); ;

                var adapted = items.Adapt<List<StaplesSourceDto>>();
                return adapted;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in GetStaplesSourceLatestReconciledDate: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> InsertBellSources(List<BellSourceDto> bellSourceDtos, int? UploadHistoryId = null)
        {
            try
            {
                TypeAdapterConfig<BellSourceDto, ReconBellUpload>.NewConfig()
                .Map(dest => dest.DealerCode, src => src.DealerCode != null ? src.DealerCode.Substring(0, Math.Min(src.DealerCode.Length, 10)) : "")
                .Map(dest => dest.OrderNumber, src => src.OrderNumber != null ? src.OrderNumber.Substring(0, Math.Min(src.OrderNumber.Length, 20)) : "")
                .Map(dest => dest.Imei, src => src.Imei != null ? src.Imei.Substring(0, Math.Min(src.Imei.Length, 30)) : "")
                .Map(dest => dest.Simserial, src => src.Simserial != null ? src.Simserial.Substring(0, Math.Min(src.Simserial.Length, 30)) : "")
                .Map(dest => dest.RebateType, src => src.RebateType != null ? src.RebateType.Substring(0, Math.Min(src.RebateType.Length, 20)) : "")
                .Map(dest => dest.Product, src => src.Product != null ? src.Product.Substring(0, Math.Min(src.Product.Length, 250)) : "")
                .Map(dest => dest.CustomerAccount, src => src.CustomerAccount != null ? src.CustomerAccount.Substring(0, Math.Min(src.CustomerAccount.Length, 25)) : "")
                .Map(dest => dest.CustomerName, src => src.CustomerName != null ? src.CustomerName.Substring(0, Math.Min(src.CustomerName.Length, 100)) : "")
                .Map(dest => dest.Lob, src => src.Lob != null ? src.Lob.Substring(0, Math.Min(src.Lob.Length, 50)) : "")
                .Map(dest => dest.ReconciledBy, src => src.ReconciledBy != null ? src.ReconciledBy.Substring(0, Math.Min(src.ReconciledBy.Length, 100)) : "")
                .Map(dest => dest.UpdatedBy, src => src.UpdatedBy != null ? src.UpdatedBy.Substring(0, Math.Min(src.UpdatedBy.Length, 100)) : "")
                //Map by mapster boolean to Short, if it is null or false , it will be 0, otherwise 1
                .Map(dest => dest.Reconciled, src => src.Reconciled == true ? (short)1 : (short)0)
                  .Map(dest => dest.CreatedDate, src => src.CreateDate)
                .Map(dest => dest.UploadHistoryId, src => UploadHistoryId);

                List<ReconBellUpload> reconBells = bellSourceDtos.Adapt<List<ReconBellUpload>>();
                await _bellDbContext.ReconBellUploads.AddRangeAsync(reconBells); //DATA TABLE
                await _bellDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in InsertBellSources: {ex.Message}");
                throw;
            }
        }

        public async Task SyncBellSourceChanges(List<BellSourceDto> bellSourceDtos)
        {
            try
            {
                foreach (var bell in bellSourceDtos)
                {
                    var bellsourceInServer = _bellDbContext.ReconBells.Find(bell.Id);
                    if (bellsourceInServer == null) continue;

                    bellsourceInServer.Comments = bell.Comments;
                    bellsourceInServer.MatchStatus = bell.MatchStatus;
                    bellsourceInServer.Reconciled = (short)(bell.Reconciled == true ? 1 : 0);
                    bellsourceInServer.ReconciledBy = bell.ReconciledBy;
                    bellsourceInServer.ReconciledDate = bell.ReconciledDate;
                    bellsourceInServer.UpdatedDate = DateTime.UtcNow;
                }
                _bellDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in SyncBellSourceChanges: {ex.Message}");
                throw;
            }
        }

        public async Task SyncStapleSourceChanges(List<StaplesSourceDto> stapleSourceDtos)
        {
            try
            {
                foreach (var staple in stapleSourceDtos)
                {
                    var staplesourceInServer = _bellDbContext.ReconStaples.Find(staple.Id);
                    if (staplesourceInServer == null) continue;

                    staplesourceInServer.Comments = staple.Comments;
                    staplesourceInServer.MatchStatus = staple.MatchStatus;
                    staplesourceInServer.Reconciled = (short)(staple.Reconciled == true ? 1 : 0);
                    staplesourceInServer.ReconciledBy = staple.ReconciledBy;
                    staplesourceInServer.ReconciledDate = staple.ReconciledDate;
                    staplesourceInServer.UpdatedDate = DateTime.UtcNow;
                }
                _bellDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in SyncStapleSourceChanges: {ex.Message}");

                throw;
            }
        }

        public async Task<string> InsertBellUploadHistory(CsvFileBellRecordsAnalysisDto uploadedPack)
        {
            try
            {
                var bellUploadHistory = new ReconBellUploadHistory();
                if (uploadedPack.IsFirstChunk)
                {
                    bellUploadHistory.UploadedDate = DateTime.Now;
                    bellUploadHistory.ProblemRowCount = uploadedPack.FailRowCount;
                    bellUploadHistory.SuccessRowCount = uploadedPack.SuccessRowCount;
                    bellUploadHistory.ProblemRows = uploadedPack.AnalysedRecords.Where(c => !c.IsSuccessful).Select(c => new { LineNumber = c.LineNumber, Errors = c.Errors }).ToJson();
                    bellUploadHistory.UploadedBy = uploadedPack.UploadBy;
                    bellUploadHistory.FileName = uploadedPack.FileName;
                    bellUploadHistory.UploadedDate = DateTime.UtcNow;
                    await _bellDbContext.ReconBellUploadHistories.AddAsync(bellUploadHistory);
                    await _bellDbContext.SaveChangesAsync();
                }
                bellUploadHistory = _bellDbContext.ReconBellUploadHistories.OrderByDescending(x => x.UploadedDate).FirstOrDefault() ?? new ReconBellUploadHistory();
                var bellRecords = uploadedPack.AnalysedRecords.Where(c => c.IsSuccessful == true).Select(c => c.BellRecord).ToList();
                var res = await InsertBellSources(bellRecords, bellUploadHistory.Id);
                if (uploadedPack.IsLastChunk)
                {
                    //RUN SP
                    _bellDbContext.Database.ExecuteSqlRaw($"EXEC [_code].[prc_UpsertReconBell] @UploadHistoryId ={bellUploadHistory.Id}");
                }
                if (res)
                {
                    return "success";
                }
                return "fail";
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in InsertBellUploadHistory: {ex.Message}");

                throw;
            }
        }

        public async Task DeleteReconBell(long id)
        {
            try
            {
                var item = _bellDbContext.ReconBells.Where(c => c.Id == id).Single();

                _bellDbContext.ReconBells.Remove(item);
                await _bellDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in DeleteReconBell: {ex.Message}");

                throw;
            }
        }

        public async Task DeleteReconStaple(long id)
        {
            try
            {
                var item = _bellDbContext.ReconStaples.Where(c => c.Id == id).Single();

                _bellDbContext.ReconStaples.Remove(item);
                await _bellDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in DeleteReconStaple: {ex.Message}");

                throw;
            }
        }

        #endregion Sync Data From LocalDB to Server

        #region LOGGING ERROR

        public async Task<List<ErrorLogDto>> GetAllLogs()
        {
            try
            {
                var items = await _bellDbContext.ReconErrorLogs.ToListAsync();

                var errorLogs = items.Adapt<List<ErrorLogDto>>();
                return errorLogs;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in GetAllLogs: {ex.Message}");
                throw;
            }
        }

        public async Task<ErrorLogDto> GetLogById(int id)
        {
            try
            {
                var items = await _bellDbContext.ReconErrorLogs.FindAsync(id);

                var errorLog = items.Adapt<ErrorLogDto>();
                return errorLog;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in GetLogById: {ex.Message}");
                throw;
            }
        }

        public async Task InsertErrorLog(ErrorLogDto log)
        {
            try
            {
                TypeAdapterConfig<ErrorLogDto, ReconErrorLog>.NewConfig().Ignore(dest => dest.Id);
                var errorLog = log.Adapt<ReconErrorLog>();
                _bellDbContext.ReconErrorLogs.Add(errorLog);

                return;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in InsertErrorLog: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> InsertErrorLogs(List<ErrorLogDto> logs)
        {
            try
            {
                TypeAdapterConfig<ErrorLogDto, ReconErrorLog>.NewConfig().Ignore(dest => dest.Id);
                var errorLog = logs.Adapt<List<ReconErrorLog>>();
                await _bellDbContext.ReconErrorLogs.AddRangeAsync(errorLog);
                await _bellDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error in InsertErrorLog: {ex.Message}");
                return false;
            }
        }

        #endregion LOGGING ERROR
    }
}