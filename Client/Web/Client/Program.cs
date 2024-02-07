using CapitalClue.Common.Models;
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

        // builder.Services.AddApiAuthorization();
        // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        string baseaddress = string.Empty;
        baseaddress = builder.Configuration["baseUrlLocal"];

        //if (builder.HostEnvironment.Environment == "Local")
        //{
        //    baseaddress = builder.Configuration["baseUrlLocal"];
        //}
        //else if (builder.HostEnvironment.Environment == "Development")
        //{
        //    baseaddress = builder.Configuration["baseUrlDev"];
        //}
        //else if (builder.HostEnvironment.Environment == "Staging")
        //{
        //    baseaddress = builder.Configuration["baseUrlStaging"];
        //}
        //else if (builder.HostEnvironment.Environment == "Production")
        //{
        //    baseaddress = builder.Configuration["baseUrlProduction"];
        //}
        builder.Services.AddSingleton(new UrlKeeper() { BaseUrl = baseaddress });
        builder.Services.AddHttpClient();

        //builder.Services.AddHttpClient("WasmBFF1.ServerAPI")
        //      .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        // Supply HttpClient instances that include access tokens when making requests to the server project
        //  builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WasmBFF1.ServerAPI"));

        //if (builder.HostEnvironment.Environment is "Production")
        //{
        //    builder.Services.AddMsalAuthentication(options =>
        //    {
        //        builder.Configuration.Bind("Production:AzureAd", options.ProviderOptions.Authentication);
        //        options.ProviderOptions.DefaultAccessTokenScopes.Add(builder.Configuration.GetSection("Production:ServerApi")["Scopes"]);
        //    });
        //}
        //else
        //{
        //    builder.Services.AddMsalAuthentication(options =>
        //    {
        //        builder.Configuration.Bind("NonProduction:AzureAd", options.ProviderOptions.Authentication);
        //        options.ProviderOptions.DefaultAccessTokenScopes.Add(builder.Configuration.GetSection("NonProduction:ServerApi")["Scopes"]);
        //    });
        //}

        builder.Services.AddSingleton<IStateContainer, StateContainer>();
        builder.Services.AddScoped<DialogService>();

        builder.Services.AddBesqlDbContextFactory<StapleSourceContext>(opts => opts.UseSqlite("Data Source=StapleSource.sqlite3"));
        builder.Services.AddTransient<ILocalDbRepository, LocalDbRepository>();
        builder.Services.AddTransient<IFetchData, FetchData>();
        builder.Services.AddTransient<ISyncData, SyncData>();

        builder.Services.AddLogging(logging =>
        {
            // var dbContextFactory = builder.Services.BuildServiceProvider().GetRequiredService<ISqliteWasmDbContextFactory<StapleSourceContext>>();
            var localDbRepository = builder.Services.BuildServiceProvider().GetRequiredService<ILocalDbRepository>();
            //var authenticationStateProvider = builder.Services.BuildServiceProvider().GetRequiredService<AuthenticationStateProvider>();
            logging.SetMinimumLevel(LogLevel.Error);
            //logging.AddProvider(new ApplicationLoggerProvider(localDbRepository, authenticationStateProvider));
        });

        var app = builder.Build();

        // Apply migrations to the database
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<StapleSourceContext>();
            await db.Database.EnsureCreatedAsync();
        }
        await app.RunAsync();
    }

}