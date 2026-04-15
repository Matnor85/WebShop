using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Presentation.DisplayService.AdminService;

namespace WebShop.Presentation.Menu.Submenu;

public class KampanjerMenu(AdminKampanj adminKampanj)
{
    
    bool _isRunning = true;

    public void ShowAdminKampanjMenu()
    {
        Console.Clear();
        Console.WriteLine("Hantera kampanjer!");
        Console.WriteLine("1 - Visa alla kampanjer");
        Console.WriteLine("2 - Skapa ny kampanj");
        Console.WriteLine("3 - Uppdatera kampanj");
        Console.WriteLine("4 - Ta bort kampanj");
        Meny.LineBreaks(2);
        Console.WriteLine("[Esc] - Tillbaka till huvudmenyn");
    }

    public async Task HanteraKampanjerAsync()
    {
        Console.Clear();
        ShowAdminKampanjMenu();
        var input = Console.ReadLine()?.Trim().ToLower();
        switch (input)
        {
            case "1":
                Console.Clear();
                await adminKampanj.ShowKampanjList();
                break;
            case "2":
                await adminKampanj.AddKampanjAsync();
                break;
            case "3":
                await adminKampanj.UpdateKampanjAsync();
                break;
            case "4":
                await adminKampanj.DeleteKampanjAsync();
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

    public async Task KampanjerMenuRunAsync()
    {
        _isRunning = true;
        while(_isRunning)
        {
            await HanteraKampanjerAsync();
        }
    }
}
