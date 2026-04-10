using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
using WebShop.Presentation.Menu.Shop_Submenu;
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
        services.AddScoped<KundMenu>();
        services.AddScoped<OrderMenu>();
        // Webbshop meny
        services.AddScoped<ShoppingCartMenu>();
        services.AddScoped<SearchProductMenu>();
        services.AddScoped<BrowseCategoriesMenu>();
        services.AddScoped<ManageOrderHistoryMenu>();

        //displayservice
        services.AddScoped<AdminKund>();
        services.AddScoped<AdminProdukt>();
        services.AddScoped<AdminKategori>();
        services.AddScoped<AdminOrder>();
        services.AddScoped<ShopBrowseCategories>();
        services.AddScoped<ShopSearchProduct>();
        services.AddScoped<ShopShoppingCart>(); 
        services.AddScoped<ShopManageOrderHistory>();
        // Logger
        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddConsole();
           
            // Dölj EF Core SQL/info-loggar. Visa bara warnings/errors från EF.
            builder.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
            builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);

            builder.SetMinimumLevel(LogLevel.Information);
        });
        // Seeder
        services.AddTransient<SeederGenerator>();
        var servicesProvider = services.BuildServiceProvider();
        
        var meny = servicesProvider.GetRequiredService<Meny>();
        await meny.MenuRunAsync();
    }
}
