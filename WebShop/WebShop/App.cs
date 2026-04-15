using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Webshop.Application.Extensions;
using Webshop.Infrastructure.EF;
using Webshop.Infrastructure.EF.Seeds;
using Webshop.Infrastructure.Extensions;
using WebShop.Presentation.Extensions;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation;

public class App
{
    public App()
    {
    }

    public static async Task RunAsync()
    {
        // För valuta API:et och för att kunna visa €-tecknet i konsolen.
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var config = new ConfigurationBuilder()
             .AddUserSecrets<App>()
             .Build();

        var services = new ServiceCollection();
        services.AddDbContext<WebshopDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        //Repositories
        services.AddRepositories();
        //Services
        services.AddServices();
        //Presentation
        services.AddPresentation();
        // Logger
        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddConsole();
           
            // Dölj EF Core SQL/info-loggar. Visa bara warnings/errors från EF.
            builder.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
            builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            builder.AddFilter("System.Net.Http.HttpClient", LogLevel.Warning);

            builder.SetMinimumLevel(LogLevel.Information);
        });
        // Seeder
        services.AddTransient<SeederGenerator>();
        var servicesProvider = services.BuildServiceProvider();
        
        var meny = servicesProvider.GetRequiredService<Meny>();
        await meny.MenuRunAsync();
    }
}
