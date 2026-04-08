using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Presentation.Menu;

public class Meny
{
    WebShopMenu _webShopMenu;
    AdminMenu _adminMenu;
    public bool _isRunning = true;

    public Meny(WebShopMenu webShopMenu, AdminMenu adminMenu)
    {
        _webShopMenu = webShopMenu;
        _adminMenu = adminMenu;
    }
    public void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("Välkommen till Webshop!");
        Console.WriteLine("1 - Till webb-Shop");
        Console.WriteLine("2 - Admin");
        Console.WriteLine("3 - Avsluta");
    }
    public async Task HandleInput()
    {
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    _webShopMenu.WebbRun();
                    break;
                case "2":
                   await _adminMenu.AdminRunAsync();
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

    public static void Wait()
    {
        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }
    public async Task MenuRunAsync()
    {
        while (_isRunning)
        {
            ShowMainMenu();
            await HandleInput();
        }
    }
}
