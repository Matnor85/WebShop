using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Presentation.DisplayService;
using WebShop.Presentation.DisplayService.AdminService;
using WebShop.Presentation.Menu;
using WebShop.Presentation.Menu.Shop_Submenu;
using WebShop.Presentation.Menu.Submenu;

namespace WebShop.Presentation.Extensions;

public static class PresentationExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
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
        services.AddScoped<KampanjerMenu>();

        //displayservice
        //Admin
        services.AddScoped<AdminKund>();
        services.AddScoped<AdminProdukt>();
        services.AddScoped<AdminKategori>();
        services.AddScoped<AdminOrder>();
        services.AddScoped<AdminKampanj>();
        //Shop
        services.AddScoped<ShopBrowseCategories>();
        services.AddScoped<ShopSearchProduct>();
        services.AddScoped<ShopShoppingCart>();
        services.AddScoped<ShopManageOrderHistory>();
        services.AddScoped<ShopKampanj>();
        return services;
    }
}
