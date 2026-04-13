using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Presentation.DisplayService.KundvagnService;
using WebShop.Presentation.DisplayService.ShopService;

namespace WebShop.Presentation.Menu.Shop_Submenu;

public class ShoppingCartMenu(ShopShoppingCart shoppingCart, Kundvagn kundvagn)
{
        bool _isRunning = true;
    public void ShowCart()
    {
            Console.Clear();
            Console.WriteLine("Din kundvagn:");
            if (kundvagn.Items.Count == 0)
            {
                Console.WriteLine("Din kundvagn är tom.");
                Meny.Wait();
                return;
            }
            shoppingCart.ShowCartSelected();
            Console.WriteLine("Alternativ [C] Ändra antal \n[T] Ta bort\n[B]Gå tillbaks");
            var input = Console.ReadLine()?.Trim().ToUpper();

            switch (input)
            {
                case "C":
                    shoppingCart.ÄndraAntal();
                    break;
                case "T":
                    shoppingCart.TaBort();
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
    
    public void ShoppingCartRun()
    {
        _isRunning = true;
        while (_isRunning)
        {
            ShowCart();
        }
    }
}
