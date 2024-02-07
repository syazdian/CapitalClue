using CapitalClue.Common.Models;
using CapitalClue.Common.Models.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;
using System.Data;
using System.Net.Http.Headers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mapster;
using System.Text.RegularExpressions;
using Bit.Besql;

namespace CapitalClue.Frontend.Web.Services.Database;

public class LocalDbRepository : ILocalDbRepository
{
    private readonly IDbContextFactory<StapleSourceContext> _dbContextFactory;

    private IStateContainer _stateContainer;
    private ILogger _logger;
}