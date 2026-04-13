using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Presentation.DisplayService.AdminService;

namespace WebShop.Presentation.Menu.Submenu;

public class KundMenu(AdminKund _adminKund)
{
    bool _isRunning = true;

    public void ShowAdminKategoriMenu()
    {
        Console.Clear();
        Console.WriteLine("Hantera kunder!");
        Console.WriteLine("1 - Visa alla kunder");
        Console.WriteLine("2 - Skapa ny kund");
        Console.WriteLine("3 - Uppdatera kund");
        Console.WriteLine("4 - Ta bort kund");
        Console.WriteLine("5 - Tillbaka till huvudmenyn");
    }

    public async Task HanteraKunderAsync()
    {
        Console.Clear();
        ShowAdminKategoriMenu();
        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                await _adminKund.ShowKundList();
                break;
            case "2":
                await _adminKund.AddKundAsync();
                break;
            case "3":
                await _adminKund.UpdateKundAsync();
                break;
            case "4":
                await _adminKund.DeleteKundAsync();
                break;
            case "5":
                _isRunning = false;
                break;
            default:
                Console.WriteLine("Ogiltigt val, försök igen.");
                break;
        }
    }

    public async Task KundMenuRunAsync()
    {
        while (_isRunning)
        {
            await HanteraKunderAsync();
        }
    }
}
