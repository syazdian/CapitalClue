using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace CapitalClue.Frontend.Web.Logging;

public class ApplicationLoggerProvider : ILoggerProvider
{
    // private readonly ISqliteWasmDbContextFactory<StapleSourceContext> _dbContextFactory;

    //private readonly IStateContainer _statecontainer;
    private readonly ILocalDbRepository _localDb;

    public AuthenticationStateProvider _authenticationStateProvider { get; }

    public ApplicationLoggerProvider(ILocalDbRepository localDb, AuthenticationStateProvider authenticationStateProvider)
    {
        _localDb = localDb;
        _authenticationStateProvider = authenticationStateProvider;

        //_statecontainer = statecontainer;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DatabaseLogger(_localDb, _authenticationStateProvider);
    }

    public void Dispose()
    {
    }
}