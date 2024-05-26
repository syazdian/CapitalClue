using Bell.Reconciliation.Frontend.Web.Services;
using CapitalClue.Common.Models;
using CapitalClue.Common.Models.Domain;
using CapitalClue.Frontend.Shared.ServiceInterfaces;
using CapitalClue.Frontend.Web.Models;
using CapitalClue.Frontend.Web.Services.Services;
using Radzen;
using Shared.Pages;

namespace CapitalClue.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        string baseaddress = string.Empty;

        //if (builder.HostEnvironment.Environment == "Local")
        //{
        //    baseaddress = builder.Configuration["baseUrlLocal"];
        //}
        //else if (builder.HostEnvironment.Environment == "Development")
        //{
        //    baseaddress = builder.Configuration["baseUrlDev"];
        //}
        // baseaddress = builder.Configuration["baseUrlLocal"];

        baseaddress = builder.Configuration["baseUrlDev"];

        builder.Services.AddSingleton(new UrlKeeper() { BaseUrl = baseaddress });
        // builder.Services.AddHttpClient();

        //   builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WasmBFF1.ServerAPI"));
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        var filterItems = GetFilterItems(builder);
        builder.Services.AddSingleton(filterItems);

        builder.Services.AddSingleton<IStateContainer, StateContainer>();
        builder.Services.AddSingleton<IProfitCalculations, ProfitCalculations>();
        builder.Services.AddScoped<DialogService>();

        // builder.Services.AddBesqlDbContextFactory<StapleSourceContext>(opts => opts.UseSqlite("Data Source=StapleSource.sqlite3"));
        // builder.Services.AddTransient<ILocalDbRepository, LocalDbRepository>();
        builder.Services.AddTransient<IFetchData, FetchData>();
        builder.Services.AddTransient<ISyncData, SyncData>();
        builder.Services.AddTransient<IFilterService, FilterService>();

        builder.Services.AddLogging(logging =>
        {
            // var dbContextFactory = builder.Services.BuildServiceProvider().GetRequiredService<ISqliteWasmDbContextFactory<StapleSourceContext>>();
            // var localDbRepository = builder.Services.BuildServiceProvider().GetRequiredService<ILocalDbRepository>();
            //var authenticationStateProvider = builder.Services.BuildServiceProvider().GetRequiredService<AuthenticationStateProvider>();
            logging.SetMinimumLevel(LogLevel.Error);
            //logging.AddProvider(new ApplicationLoggerProvider(localDbRepository, authenticationStateProvider));
        });

        var app = builder.Build();

        // Apply migrations to the database

        await app.RunAsync();
    }

    private static FilterItemsDisplay GetFilterItems(WebAssemblyHostBuilder builder)
    {
        FilterItemsDisplay filterItems = new FilterItemsDisplay();
        filterItems.StockFilterDisplayObj.Currencies = builder.Configuration.GetSection("Currency").Get<List<string>>();
        filterItems.StockFilterDisplayObj.Stocks = builder.Configuration.GetSection("Stock").Get<List<string>>();
        filterItems.PropertyFilterObj.Cities = builder.Configuration.GetSection("City").Get<List<string>>();
        filterItems.PropertyFilterObj.PropertyType = builder.Configuration.GetSection("PropertyType").Get<List<string>>();

        return filterItems;
    }
}