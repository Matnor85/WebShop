using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Presentation.DisplayService.KundvagnService;
using WebShop.Presentation.DisplayService.ShopService;
using WebShop.Presentation.Menu.Submenu;

namespace WebShop.Presentation.Menu.Shop_Submenu;

public class ShoppingCartMenu(ShopShoppingCart shoppingCart, Kundvagn kundvagn, CheckOutMenu checkOut)
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
                if (CheckShoppingCart())
                shoppingCart.ChangeStock();
                break;
            case "T":
                if (CheckShoppingCart())
                    shoppingCart.RemoveItem();
                break;
            case "P":
                // Add payment logic here
                if (CheckShoppingCart())
                    Console.WriteLine("Betalning genomförd! Tack för ditt köp.");
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

    private bool CheckShoppingCart()
    {
        if (kundvagn.Items.Count == 0)
        {
            Console.WriteLine("Din kundvagn är tom.");
            Meny.Wait();
            return false;
        }
        return true;
    }

    private bool PrepareCartDisplay()
    {
        Console.Clear();
        Console.WriteLine("=== Din kundvagn ===");
        if (kundvagn.Items.Count == 0)
        {
            Console.WriteLine("Din kundvagn är tom.");
            //Meny.Wait();
            // return false;
        }
        shoppingCart.ShowCartSelected();
        Console.WriteLine("Alternativ [C] Ändra antal \t[T] Ta bort\t[B]Gå tillbaka\t[P] Betala");
        return true;
    }

    public async Task ShoppingCartRun()
    {
        _isRunning = true;
        while (_isRunning)
        {
            await ShowCart();
        }
    }
}
