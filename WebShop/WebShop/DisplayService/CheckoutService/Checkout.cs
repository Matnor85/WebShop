using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using WebShop.Presentation.DisplayService.Helpers;
using WebShop.Presentation.DisplayService.ValutaApi;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService.CheckoutService;

public class CheckOut(IFraktOmbudService fraktService, IKundService kundService, ValutaSession valutaSession, KundInputHelper kundInputHelper)
{
    public async Task<Kund> CreateCustomerAsync()
    {
        Console.WriteLine("=== Kassa ===");
        Console.WriteLine("skapa konto för att beräkna fraktkostnad");
        var kund =  kundInputHelper.KundInput();
        return await kundService.AddAsync(kund);
    }

    public async Task<FraktOmbud?> ChooseDelivery()
    {
        var fraktOmbudList = await fraktService.GetAllAsync();

        if (!DataValidering.ValidateList(fraktOmbudList, "Inga fraktombud tillgängliga"))
            return null;
        ShowFraktOmbudList(fraktOmbudList);
        var input = Console.ReadLine()?.Trim().ToLower();
        if (!DataValidering.ValidateListChoice(input!, fraktOmbudList.Count, out int val))
        {
            Meny.Wait();
            return null;
        }
        return fraktOmbudList[val - 1];
    }

    private void ShowFraktOmbudList(List<FraktOmbud> fraktOmbudList)
    {
        Console.WriteLine("=== Välj fraktmetod ===");
        for (int i = 0; i < fraktOmbudList.Count; i++)
        {
            Console.WriteLine($" {i + 1}. {fraktOmbudList[i].Namn} - {valutaSession.FormatPris(fraktOmbudList[i].Pris)}");
        }
    }

    
}
