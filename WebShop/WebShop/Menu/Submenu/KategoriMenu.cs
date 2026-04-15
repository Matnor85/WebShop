using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Presentation.DisplayService.AdminService;

namespace WebShop.Presentation.Menu.Submenu;

public class KategoriMenu(AdminKategori _adminKategori)
{
    bool _isRunning = true;
    
    public void ShowAdminKategoriMenu()
    {
        Console.Clear();
        Console.WriteLine("Hantera kategorier!");
        Console.WriteLine("1 - Visa alla kategorier");
        Console.WriteLine("2 - Skapa ny kategori");
        Console.WriteLine("3 - Uppdatera kategori");
        Console.WriteLine("4 - Ta bort kategori");
        Meny.LineBreaks(2);
        Console.WriteLine("[Esc] - Tillbaka till huvudmenyn");
    }

    public async Task HanteraKategorierAsync()
    {
        Console.Clear();
        ShowAdminKategoriMenu();
        var input = Console.ReadLine()?.Trim().ToLower();
        switch (input)
        {
            case "1":
                Console.Clear();
                await _adminKategori.ShowKategoriList();
                break;
            case "2":
                await _adminKategori.AddKategoriAsync();
                break;
            case "3":
                await _adminKategori.UpdateKategoriAsync();
                break;
            case "4":
                await _adminKategori.DeleteKategoriAsync();
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

    public async Task KategoriMenuRunAsync()
    {
        while (_isRunning)
        {
            await HanteraKategorierAsync();
        }
    }
}
