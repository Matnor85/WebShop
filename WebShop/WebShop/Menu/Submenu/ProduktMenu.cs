using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Presentation.DisplayService.AdminService;

namespace WebShop.Presentation.Menu.Submenu;

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
        Meny.LineBreaks(2);
        Console.WriteLine("[Esc] - Tillbaka till adminmenyn");
    }
    public async Task HanteraProdukterAsync()
    {
        Console.Clear();
        ShowProduktMenu();
        var input = Console.ReadLine()?.Trim().ToLower();
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
            case "esc":
            case "escape":
            case "avbryt":
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
