using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Application.Services;
using Webshop.Domain.Interfaces;
using Webshop.Infrastructure.EF;
using Webshop.Infrastructure.EF.Repositories;
using Webshop.Infrastructure.EF.Seeds;
using WebShop.Presentation.DisplayService;
using WebShop.Presentation.Menu;
using WebShop.Presentation.Menu.Submenu;

namespace WebShop.Presentation;

public class App
{
    public App()
    {
    }

    public static async Task RunAsync()
    {
        var config = new ConfigurationBuilder()
             .AddUserSecrets<App>()
             .Build();

        var services = new ServiceCollection();
        services.AddDbContext<WebshopDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
        
        //Services
        services.AddScoped<IProduktRepository, ProduktRepository>();
        services.AddScoped<IProduktService, ProduktService>();

        services.AddScoped<IProduktOrderRepository, ProduktOrderRepository>();
        services.AddScoped<IProduktOrderService, ProduktOrderService>();

        services.AddScoped<IKategoriRepository, KategoriRepository>();
        services.AddScoped<IKategoriService, KategoriService>();

        services.AddScoped<IKundRepository, KundRepository>();
        services.AddScoped<IKundService, KundService>();

        services.AddScoped<ILeverantörRepository, LeverantörRepository>();
        services.AddScoped<ILeverantörService, LeverantörService>();

        services.AddScoped<IFraktOmbudRepository, FraktOmbudRepository>();
        services.AddScoped<IFraktOmbudService, FraktOmbudService>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();

        //meny
        services.AddScoped<Meny>();
        services.AddScoped<WebShopMenu>();
        services.AddScoped<AdminMenu>();
        services.AddScoped<KategoriMenu>();
        services.AddScoped<ProduktMenu>();
        services.AddScoped<AdminProdukt>();
        services.AddScoped<AdminKategori>();

        var servicesProvider = services.BuildServiceProvider();
        
        var meny = servicesProvider.GetRequiredService<Meny>();
        await meny.MenuRunAsync();
    }
}
