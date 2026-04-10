using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Presentation.DisplayService;

namespace WebShop.Presentation.Menu.Shop_Submenu;

public class ShoppingCartMenu(ShopShoppingCart shoppingCart)
{
        bool _isRunning = true;
    public void ShoppingCartDisplay()
    {
        Console.WriteLine("=== Kundvagn val ===");
        Console.WriteLine("1 - Ändra antal");
        Console.WriteLine("2 - Ta bort produkt");
        Console.WriteLine("3 - Gå vidare till kassan");
        Console.WriteLine("4 - Tillbaka till webb-shopmenyn");
    }
    public void ShoppingCart()
    {
        //Console.WriteLine("=== Kundvagn ===");

        //foreach (var item in ShoppingCartService.CartItems)
        //{
        //    Console.WriteLine($"{item.Product.Name} - Antal: {item.Quantity} - Pris: {item.Product.Price * item.Quantity}");
        //}
        //ShoppingCartMenu();
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

                break;
            case "4":

                break;
            case "5":
                _isRunning = false;
                break;
            default:
                Console.WriteLine("Ogiltigt val, försök igen.");
                break;
        }

    }
    public void ShoppingCartRun()
    {
        _isRunning = true;
        while (_isRunning)
        {
            ShoppingCartDisplay();
            HandleInput();
        }
    }
}
