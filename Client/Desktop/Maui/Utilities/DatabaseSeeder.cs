using CapitalClue.Frontend.Desktop.Data.Seeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalClue.Frontend.Desktop.Maui.Utilities;

public static class DatabaseSeeder
{
    public static void SeedDatabase(this MauiApp mauiApp)
    {
        using var scope = mauiApp.Services.CreateScope();
        var context = scope.ServiceProvider.GetService(typeof(StapleSourceContext)) as StapleSourceContext;

        if (context == null) return;

        var stapleSource = StaplesSeeder.StapleSourceToSeed.Where(x => !context.StaplesSources.Any(y => y.Phone == x.Phone))?.ToList();
        context.StaplesSources.AddRange(stapleSource);

        var bellSource = BellSeeder.BellSourceToSeed.Where(x => !context.BellSources.Any(y => y.Phone == x.Phone))?.ToList();
        context.BellSources.AddRange(bellSource);

        context.SaveChanges();
    }
}