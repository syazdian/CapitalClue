using CapitalClue.Common.Models;
using CapitalClue.Common.Models.Domain;
using CapitalClue.Frontend.Desktop.Maui.Models;
using CapitalClue.Frontend.Desktop.Maui.Utilities;
using CapitalClue.Frontend.Desktop.Models;
using CapitalClue.Frontend.Desktop.Services.Database;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Radzen;
using System.Reflection;

namespace CapitalClue.Frontend.Desktop.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        var executingAssembly = Assembly.GetExecutingAssembly();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        //var a = Assembly.GetExecutingAssembly();
        //using var stream = a.GetManifestResourceStream($"appsettings.json");
        //var config = new ConfigurationBuilder()
        //            .AddJsonStream(stream)
        //            .Build();
        //builder.Configuration.AddConfiguration(config);

        var config = new ConfigurationBuilder()
                    .SetBasePath(Path.GetDirectoryName(executingAssembly.Location))
                    .AddJsonFile($"appsettings.json")
                    .Build();
        builder.Configuration.AddConfiguration(config);

        var settings = builder.Configuration.GetRequiredSection("Settings").Get<Settings>(); ;

        string baseaddress = string.Empty;

        //if (builder.HostEnvironment.Environment == "Local")
        //{
        baseaddress = settings.baseUrlLocal;
        //}
        //else if (builder.HostEnvironment.Environment == "Development")
        //{
        //    baseaddress = settings.baseUrlDev;
        //}
        //else if (builder.HostEnvironment.Environment == "Staging")
        //{
        //    baseaddress = settings.baseUrlStaging;
        //}
        //else if (builder.Configuration..Environment == "Production")
        //{
        //    baseaddress = settings.baseUrlProduction;
        //}

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseaddress) });
        builder.Services.AddSingleton(new UrlKeeper() { BaseUrl = baseaddress });

        //builder.Services.AddHttpClient<ISyncData, SyncData>(client =>
        //{
        //    client.BaseAddress = new Uri( settings.BaseAddress);
        //});
        var filterItems = GetFilterItems(builder);
        builder.Services.AddSingleton(filterItems);

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddRadzenComponents();
        builder.Services.AddDbContext<StapleSourceContext>(a => a.UseSqlite(ProjectConfig.DatabasePath));

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddScoped<DialogService>();
        builder.Services.AddSingleton<IStateContainer, StateContainer>();
        builder.Services.AddTransient<IFilterService, FilterService>();
        builder.Services.AddTransient<ILocalDbRepository, LocalDbRepository>();
        builder.Services.AddTransient<ISyncData, SyncData>();

        builder.Services.AddTransient<IFetchData, FetchData>();
        builder.Services.AddTransient<ICsvExport, CsvExport>();

        // builder.Services.AddTransient<IInjectBellSource, InjectBellSource>();

        var app = builder.Build();
        //app.SeedDatabase();
        return app;
    }

    private static FilterItemsDisplay GetFilterItems(MauiAppBuilder builder)
    {
        FilterItemsDisplay filterItems = new FilterItemsDisplay();

        LoB loBWireless = new LoB()
        {
            Name = "Wireless",
            SubLoBs = builder.Configuration.GetSection("FilterItems:LoB:Wireless").Get<List<string>>()
        };

        filterItems.LoBs.Add(loBWireless);
        LoB loBWireline = new LoB()
        {
            Name = "Wireline",
            SubLoBs = builder.Configuration.GetSection("FilterItems:LoB:Wireline").Get<List<string>>()
        };
        filterItems.LoBs.Add(loBWireline);
        filterItems.Brands = builder.Configuration.GetSection("FilterItems:Brand").Get<List<string>>();
        filterItems.RebateTypes = builder.Configuration.GetSection("FilterItems:RebateType").Get<List<string>>();

        // filterItems.Locations = builder.Configuration.GetSection("FilterItems:Location").Get<List<string>>();

        return filterItems;
    }
}