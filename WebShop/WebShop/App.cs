using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Infrastructure.EF;

namespace WebShop.Presentation;

public class App
{
    public App()
    {
    }

    public static void Run()
    {
       var config = new ConfigurationBuilder()
            .AddUserSecrets<App>()
            .Build();

        var services = new ServiceCollection();
        services.AddDbContext<WebshopDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        var servicesProvider = services.BuildServiceProvider();

        using var db = servicesProvider.GetRequiredService<WebshopDbContext>();
    }
}
