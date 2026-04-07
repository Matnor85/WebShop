using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShop.Presentation.UI;
using Webshop.Infrastructure.EF;
using Webshop.Infrastructure.EF.Seeds;

namespace WebShop.Presentation;

public class App
{
    public App()
    {
    }

    public static void Run(bool menuOnly = false)
    {
        if (menuOnly)
        {
            MenuWindow.Start();
            return;
        }

        var config = new ConfigurationBuilder()
             .AddUserSecrets<App>()
             .Build();

        var services = new ServiceCollection();
        services.AddDbContext<WebshopDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        var servicesProvider = services.BuildServiceProvider();

        using var db = servicesProvider.GetRequiredService<WebshopDbContext>();
        WebShopSeeder.Seed(db);

        var popularProducts = db.Produkter
            .AsNoTracking()
            .Take(6)
            .Select(p => new ProductPreview(p.Namn, p.Pris))
            .ToList();

        MenuWindow.Start(popularProducts);
    }
}
