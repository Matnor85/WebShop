using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Infrastructure.EF.Seeds;

public class SeederMethod
{
    public void AddSeed()
    {
        // Hämta konfigurationen från appsettings.json
        var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: true)
        .Build();
        // Skapa en service collection och konfigurera tjänsterna
        var services = new ServiceCollection();
        services.AddLogging();

        var conn = configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrWhiteSpace(conn))
        {
            services.AddDbContext<WebshopDbContext>(opts => opts.UseSqlServer(conn));
            services.AddTransient<SeederGenerator>();
        }

        using var provider = services.BuildServiceProvider();
        try
        {
            using var scope = provider.CreateScope();
            var sp = scope.ServiceProvider;
            var seeder = sp.GetService<SeederGenerator>();
            if (seeder != null)
            {
                seeder.SeedAsync().GetAwaiter().GetResult();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seeding misslyckades: {ex.Message}\n{ex.StackTrace}");
        }
    }
}
