using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebShop.Presentation;
using WebShop.Presentation.Menu;
using Webshop.Infrastructure.EF;
using Webshop.Infrastructure.EF.Seeds;

namespace WebShop;

internal class Program
{
    static void Main(string[] args)
    {
        Meny Meny = new Meny();
        Meny.MenuRun();
        //App.Run();
    }
}
