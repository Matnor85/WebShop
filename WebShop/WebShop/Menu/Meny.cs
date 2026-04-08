using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Presentation.Menu;

public class Meny
{
    WebShopMenu _webShopMenu = new WebShopMenu();
    AdminMenu _adminMenu = new AdminMenu();
    public bool _isRunning = true;
    public void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("Välkommen till Webshop!");
        Console.WriteLine("1 - Till webb-Shop");
        Console.WriteLine("2 - Admin");
        Console.WriteLine("3 - Avsluta");
    }
    public void HandleInput()
    {
        
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    _webShopMenu.WebbRun();
                    break;
                case "2":
                    _adminMenu.AdminRun();
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
    public void MenuRun()
    {
        while (_isRunning)
        {
            ShowMainMenu();
            HandleInput();
        }
    }
}
