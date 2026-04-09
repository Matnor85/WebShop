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
        // Build configuration manually and register services without using Generic Host
        var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: true)
        .Build();

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
        catch (Exception)
        {
            // continue startup even if seeding fails
        }
    }
}
