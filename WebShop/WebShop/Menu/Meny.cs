using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Presentation.Menu;

public class Meny
{
    public bool _isRunning = true;
    public void ShowMainMenu()
    {
        Console.WriteLine("Välkommen till Webshop!");
        Console.WriteLine("1 - Till webb-Shop");
        Console.WriteLine("2 - Admin");
        Console.WriteLine("3 - Avsluta");
    }
    public void HandleInput()
    {
        while (true)
        {
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    WebShopMenu.ShowWebShopMenu();
                    break;
                case "2":
                    AdminMenu.ShowAdminMenu();
                    break;
                case "3":
                    Console.WriteLine("Tack för att du besökte vår webshop. Ha en bra dag!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    break;
            }
        }
    }
    public void MenuRun()
    {
        while (_isRunning)
        {
        ShowMainMenu();
        HandleInput();

        }
    }
}
