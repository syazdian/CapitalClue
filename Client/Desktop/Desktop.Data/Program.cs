namespace Bell.Reconciliation.Frontend.Desktop.Data;

public static class Program
{
    public static void Main(string[] args)
    {
    }
}

public class DbContextMigrationFactory : IDesignTimeDbContextFactory<StapleSourceContext>
{
    public StapleSourceContext CreateDbContext(string[] args)
    {
        var dbPath = ProjectConfig.DatabasePath;
        var builder = new DbContextOptionsBuilder<StapleSourceContext>();
        builder.UseSqlite(dbPath);
        return new StapleSourceContext(builder.Options);
    }
}