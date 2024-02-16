using Microsoft.EntityFrameworkCore;
using CapitalClue.Common.Models;

namespace CapitalClue.Frontend.Web.Database
{
    public class StapleSourceContext : DbContext
    {
        public StapleSourceContext(DbContextOptions<StapleSourceContext> opts) : base(opts)
        {
        }
    }
}