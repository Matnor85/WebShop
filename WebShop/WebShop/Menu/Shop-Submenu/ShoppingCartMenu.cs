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

        var input = Console.ReadLine()?.Trim().ToLower();
        switch (input)
        {
            case "c":
                if (CheckShoppingCart())
                    shoppingCart.ChangeStock();
                break;
            case "t":
                if (CheckShoppingCart())
                    shoppingCart.RemoveItem();
                break;
            case "k":
                if (CheckShoppingCart())
                    await checkOut.CheckOutRun();
                break;
            case "0":
                _isRunning = false;
                break;

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
        }
        shoppingCart.ShowCartSelected();
        Console.WriteLine("\nAlternativ\n[C] Ändra antal   [T] Ta bort   [0] Gå tillbaka   [K] Betala");
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