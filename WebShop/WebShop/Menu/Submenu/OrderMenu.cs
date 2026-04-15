using WebShop.Presentation.DisplayService.AdminService;

namespace WebShop.Presentation.Menu.Submenu;

public class OrderMenu(AdminOrder _adminOrder)
{
    bool _isRunning = true;

    public void ShowAdminOrderMenu()
    {
        Console.Clear();
        Console.WriteLine("Hantera order!");
        Console.WriteLine("1 - Visa alla order");
        Meny.LineBreaks(2);
        Console.WriteLine("0 - Tillbaka till huvudmenyn");
    }

    public async Task HanteraOrderAsync()
    {
        Console.Clear();
        ShowAdminOrderMenu();
        var input = Console.ReadLine()?.Trim().ToLower();
        switch (input)
        {
            case "1":
                Console.Clear();
                await _adminOrder.ShowOrderList();
                break;
            case "0":
                _isRunning = false;
                break;
            default:
                Console.WriteLine("Ogiltigt val, försök igen.");
                break;
        }
    }

    public async Task OrderMenuRunAsync()
    {
        while (_isRunning)
        {
            await HanteraOrderAsync();
        }
    }
}
