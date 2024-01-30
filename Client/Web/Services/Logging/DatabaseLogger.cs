using CapitalClue.Common.Models.Domain;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace CapitalClue.Frontend.Web.Logging;

public class DatabaseLogger : ILogger
{
    // private readonly IStateContainer _statecontainer;
    private readonly ILocalDbRepository _localDb;

    public AuthenticationStateProvider _authenticationStateProvider { get; }

    public DatabaseLogger(ILocalDbRepository localDb, AuthenticationStateProvider authenticationStateProvider)//, IStateContainer statecontainer)
    {
        _localDb = localDb;
        _authenticationStateProvider = authenticationStateProvider;
        //_statecontainer = statecontainer;
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
        var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
        var username = user.Identity.Name;

        ErrorLogDto log = new();
        log.LogLevel = logLevel.ToString();
        log.EventName = eventId.Name;
        log.ExceptionMessage = exception?.Message;
        log.StackTrace = exception?.StackTrace;
        log.Source = "Client";
        log.CreatedDate = DateTime.UtcNow;
        log.UserId = username;

        _localDb.InsertErrorLogs(log);
    }
}