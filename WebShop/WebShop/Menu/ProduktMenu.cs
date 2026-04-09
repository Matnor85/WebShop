using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Presentation.DisplayService;

namespace WebShop.Presentation.Menu;

public class ProduktMenu(AdminProdukt _adminProdukt)
{
    bool _isRunning = true;
    public void ShowProduktMenu()
    {
        Console.Clear();
        Console.WriteLine("Hantera produkter");
        Console.WriteLine("1 - Visa alla produkter");
        Console.WriteLine("2 - Lägg till produkt");
        Console.WriteLine("3 - Uppdatera produkt");
        Console.WriteLine("4 - Ta bort produkt");
        Console.WriteLine("5 - Tillbaka till adminmenyn");
    }
    public async Task HanteraProdukterAsync()
    {
        Console.Clear();
        ShowProduktMenu();
        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                await _adminProdukt.ShowProduktList();
                break;
            case "2":
                await _adminProdukt.AddProduktAsync();
                break;
            case "3":
                await _adminProdukt.UpdateProdukt();
                break;
            case "4":
                await _adminProdukt.DeleteProdukt();
                break;
            case "5":
                _isRunning = false;
                break;
            default:
                Console.WriteLine("Ogiltigt val, försök igen.");
                break;
        }
    }
    public async Task ProduktMenuRunAsync()
    {

        while (_isRunning)
        {
            await HanteraProdukterAsync();
        }
    }
}
