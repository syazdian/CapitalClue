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