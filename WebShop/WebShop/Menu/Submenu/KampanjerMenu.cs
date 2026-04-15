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
        ConsoleKeyInfo key = Console.ReadKey(true);
        switch (key.Key)
        {
            case ConsoleKey.D1:
                Console.Clear();
                await adminKampanj.ShowKampanjList();
                break;
            case ConsoleKey.D2:
                await adminKampanj.AddKampanjAsync();
                break;
            case ConsoleKey.D3:
                await adminKampanj.UpdateKampanjAsync();
                break;
            case ConsoleKey.D4:
                await adminKampanj.DeleteKampanjAsync();
                break;
            case ConsoleKey.Escape:
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
