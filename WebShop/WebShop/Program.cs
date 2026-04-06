using Microsoft.Extensions.Configuration;
using WebShop.Presentation;

namespace WebShop;

internal class Program
{
    static void Main(string[] args)
    {
        var menuOnly = args.Any(a => string.Equals(a, "--menu-only", StringComparison.OrdinalIgnoreCase));
        App.Run(menuOnly);
    }
}
