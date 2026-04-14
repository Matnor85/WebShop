using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Presentation.DisplayService.KundvagnService;
using WebShop.Presentation.DisplayService.ShopService;

namespace WebShop.Presentation.Menu.Shop_Submenu;

public class ShoppingCartMenu(ShopShoppingCart shoppingCart, Kundvagn kundvagn)
{
        bool _isRunning = true;
    public async Task ShowCart()
    {
        if (!PrepareCartDisplay())
            return;
        
        var input = Console.ReadLine()?.Trim().ToUpper();
        switch (input)
        {
            case "C":
                shoppingCart.ChangeStock();
                break;
            case "T":
                shoppingCart.RemoveItem();
                break;
            case "P":
                // Add payment logic here
                break;
            case "B":
                _isRunning = false;
                return;

            default:
                Console.WriteLine("Ogiltigt val. Försök igen.");
                Meny.Wait();
                break;
        }
    }

    private bool PrepareCartDisplay()
    {
        Console.Clear();
        Console.WriteLine("Din kundvagn:");
        if (kundvagn.Items.Count == 0)
        {
            Console.WriteLine("Din kundvagn är tom.");
            Meny.Wait();
            return false;
        }
        shoppingCart.ShowCartSelected();
        Console.WriteLine("Alternativ [C] Ändra antal \n[T] Ta bort\n[B]Gå tillbaka\n[P] Betala");
        return true;
    }

    public async Task ShoppingCartRun()
    {
        _isRunning = true;
        while (_isRunning)
        {
            ShowCart();
        }
    }
}
