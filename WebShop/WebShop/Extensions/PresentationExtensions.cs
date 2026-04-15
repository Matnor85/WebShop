using Microsoft.Extensions.DependencyInjection;
using WebShop.Presentation.DisplayService.AdminService;
using WebShop.Presentation.DisplayService.BetalVyService;
using WebShop.Presentation.DisplayService.CheckoutService;
using WebShop.Presentation.DisplayService.Helpers;
using WebShop.Presentation.DisplayService.KundvagnService;
using WebShop.Presentation.DisplayService.ShopService;
using WebShop.Presentation.DisplayService.ValutaApi;
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
        services.AddScoped<CheckOutMenu>();
        services.AddScoped<BetalMenu>();
        services.AddScoped<ShoppingCartMenu>();
        services.AddScoped<BrowseCategoriesMenu>();
        services.AddScoped<KampanjerMenu>();

        //displayservice
        //Helpers
        services.AddScoped<KundInputHelper>();
        //API
        services.AddScoped<ValutaSession>();
        services.AddScoped<ValutaDisplay>();
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
        services.AddScoped<ShopKampanj>();
        services.AddScoped<BetalVy>();
        services.AddScoped<CheckOut>();

        //Kundvagn
        services.AddScoped<Kundvagn>();
        return services;
    }
}