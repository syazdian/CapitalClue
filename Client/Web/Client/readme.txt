how to fetch the browser DB:
URL.createObjectURL(await (await (await caches.open("SqliteWasmHelper")).match("/data/cache/StapleSource.sqlite3")).blob());


 Scaffold-DbContext "Data Source=recbell.database.windows.net;Initial Catalog=recbell;User ID=sharif;Password=biaTelegram123;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -f
	
	
	Scaffold-DbContext "Data Source=efi-stg-database-smi.77e4410e91a5.database.windows.net;Initial Catalog=BELL_INTEGRATION_DEV;Persist Security Info=True;User ID=log_util_bell;Password=test;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -f


===============================================

DATABASE SWITCHING STEPS:
1. Index.html
2. wwroot/appsetting
3. appseting in server
4. data folder - db context

===============================================
INGORE AUTHENTICATION:

1.app.razzr
2. Exclude "Shared\Header\LoginDisplay.razor"
3. client/program.cs
        UnComment this line : builder.Services.AddHttpClient(); 
        comment this lines:
		// builder.Services.AddApiAuthorization();
        builder.Services.AddHttpClient("WasmBFF1.ServerAPI")
               .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
        // Supply HttpClient instances that include access tokens when making requests to the server project
        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WasmBFF1.ServerAPI"));
        builder.Services.AddMsalAuthentication(options =>
        {
            builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
            options.ProviderOptions.DefaultAccessTokenScopes.Add(builder.Configuration.GetSection("ServerApi")["Scopes"]);
        });
4. server/program.cs
    comment this line : builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

===============================================
source fo editingJson js library

https://github.com/josdejong/jsoneditor
https://jsoneditoronline.org/#left=local.wubapi&right=local.huyenu
https://cdnjs.com/libraries/jsoneditor