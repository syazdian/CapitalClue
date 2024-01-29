namespace Server.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        //builder.Services.AddControllers();
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseWebAssemblyDebugging();
        }

        app.UseBlazorFrameworkFiles();

        app.UseStaticFiles();
        app.UseHttpsRedirection();

        app.UseAuthorization();

           app.UseRouting();

        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}