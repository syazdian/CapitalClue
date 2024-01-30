using CapitalClue.Web.Server.Data.Sqlserver;

namespace CapitalClue.Web.Server.Logging;

public class ApplicationLoggerProvider : ILoggerProvider
{
    private ServerDbRepository _dbRepo;

    public ApplicationLoggerProvider(ServerDbRepository dbRepo)
    {
        _dbRepo = dbRepo;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DatabaseLogger(_dbRepo);
    }

    public void Dispose()
    {
    }
}