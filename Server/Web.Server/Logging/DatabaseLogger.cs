using CapitalClue.Web.Server.Data.Sqlserver;
using CapitalClue.Web.Server.Model;
using System.Security.AccessControl;

namespace CapitalClue.Web.Server.Logging;

public class DatabaseLogger : ILogger
{
    private readonly ServerDbRepository _dbRepo;

    public DatabaseLogger(ServerDbRepository dbRepo)
    {
        _dbRepo = dbRepo;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        //Need to figure out how can we get userid for the logger when using JWT authorization

        ErrorLogDto log = new();
        log.LogLevel = logLevel.ToString();
        log.UserId = UserId.UserName;
        log.ExceptionMessage = exception?.Message;
        log.StackTrace = exception?.StackTrace;
        log.Source = "Server";
        log.CreatedDate = DateTime.UtcNow;

        await _dbRepo.InsertErrorLog(log);
    }
}