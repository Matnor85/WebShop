using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Presentation.Menu;

public class Meny(WebShopMenu webShopMenu, AdminMenu adminMenu)
{
    public bool _isRunning = true;

    public void ShowMainMenu()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Console.WriteLine("1 - Till webb-Shop");
        Console.WriteLine("2 - Admin");
        // Meny.LineBreaks(2);
        Console.WriteLine("0 - Avsluta");
    }
    public async Task HandleInput()
    {
        var input = Console.ReadLine()?.Trim().ToLower();
        switch (input)
            {
                case "1":
                   await webShopMenu.WebbRun();
                    break;
                case "2":
                   await adminMenu.AdminRunAsync();
                    break;
                case "0":
                    Console.WriteLine("Tack för att du besökte vår webshop. Ha en bra dag!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    break;
            
            }
    }
    public static void WelcomeText()
    {
        Console.WriteLine("Välkommen till vår webbshop!");
        Console.WriteLine("Här kan du hitta de senaste produkterna och erbjudandena.");
    }
    public static void LineBreaks(int antal)
    {
        for (int i = 0; i < antal; i++)
        {
            Console.WriteLine();
        }
    }

    public static void Wait()
    {
        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ReadLine();
    }
    public static void CreateLines(char input, int length)
    {
        Console.WriteLine(new string(input, length));
    }
    public async Task MenuRunAsync()
    {
        _isRunning = true;
        while (_isRunning)
        {
            ShowMainMenu();
            await HandleInput();
        }
    }
}
