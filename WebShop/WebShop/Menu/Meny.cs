using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Presentation.Menu;

public class Meny(WebShopMenu webShopMenu, AdminMenu adminMenu)
{
    public bool _isRunning = true;

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
                    webShopMenu.WebbRun();
                    break;
                case "2":
                   await adminMenu.AdminRunAsync();
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
