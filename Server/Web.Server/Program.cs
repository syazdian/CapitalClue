using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System.Reflection;

namespace CapitalClue.Web.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //       .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

        //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //     .AddJwtBearer(options =>
        //     {
        //         options.RequireHttpsMetadata = false;
        //     })
        //    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"), "AzureAd", "BearerAzureAd");

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        var app = builder.Build();
        app.UseCors(x => x
          .AllowAnyMethod()
          .AllowAnyHeader()
          .SetIsOriginAllowed(origin => true) // allow any origin
          .AllowCredentials());

        var lf = app.Services.GetRequiredService<ILoggerFactory>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();

        app.UseStaticFiles();
        // app.UseBlazorFrameworkFiles("/BellServices/Reconciliation");

        // app.UseStaticFiles("/BellServices/Reconciliation");

        app.UseRouting();

        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}