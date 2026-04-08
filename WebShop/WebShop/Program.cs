using Microsoft.Extensions.Configuration;
using WebShop.Presentation;
using WebShop.Presentation.Menu;

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
