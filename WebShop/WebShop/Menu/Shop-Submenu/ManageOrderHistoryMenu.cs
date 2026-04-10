using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Presentation.DisplayService;

namespace WebShop.Presentation.Menu.Shop_Submenu;

public class ManageOrderHistoryMenu(ShopManageOrderHistory shopManageOrderHistory)
{
    bool _isRunning = true;
    public static void ManageOrderHistory()
    {
        Console.WriteLine("=== Användarprofiler och orderhistorik ===");
        Console.WriteLine("1 - Se orderhistorik");
        Console.WriteLine("2 - Se användarprofiler");
        Console.WriteLine("3 - Tillbaka till webb-shopmenyn");
    }
    public void HandleInput()
    {
        var input = Console.ReadLine();
        switch (input)
        {
            case "1":

                break;
            case "2":

                break;
            case "3":
                _isRunning = false;
                break;
            default:
                Console.WriteLine("Ogiltigt val, försök igen.");
                break;
        }
    }
    public void ManageOrderHistoryRun()
    {
        _isRunning = true;
        while (_isRunning)
        {
            ManageOrderHistory();
            HandleInput();
        }
    }
}
