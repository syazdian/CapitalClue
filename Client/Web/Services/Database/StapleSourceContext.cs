using Microsoft.EntityFrameworkCore;
using CapitalClue.Common.Models;

namespace CapitalClue.Frontend.Web.Database
{
    public class StapleSourceContext : DbContext
    {
        public StapleSourceContext(DbContextOptions<StapleSourceContext> opts) : base(opts)
        {
        }

        public DbSet<BellSourceDto> BellSources { get; set; } = null!;
        public DbSet<StaplesSourceDto> StaplesSources { get; set; } = null!;
        public DbSet<SyncLogsDto> SyncLogs { get; set; } = null!;
        public DbSet<ErrorLogDto> ErrorLogs { get; set; } = null!;
        public DbSet<StoreDto> Store { get; set; } = null!;
        public DbSet<FetchHistoryDto> FetchHistory { get; set; } = null!;

        public DbSet<OnlyInBellDto> OnlyInBell { get; set; } = null!;
        public DbSet<OnlyInStaplesDto> OnlyInStaples { get; set; } = null!;
        public DbSet<CompareBellStapleDto> CompareBellStaple { get; set; } = null!;
    }
}