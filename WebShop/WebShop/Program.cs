using Microsoft.Extensions.Configuration;
using WebShop.Presentation;
using WebShop.Presentation.Menu;

namespace WebShop;

internal class Program
{
    static async Task Main(string[] args)
    {
        await App.RunAsync();
    }
}
