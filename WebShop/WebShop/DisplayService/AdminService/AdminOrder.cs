using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
using WebShop.Presentation.DisplayService.ValutaApi;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService.AdminService;

public class AdminOrder(IOrderService orderService, IKundService kundService, ValutaSession valutaSession)
{
    public async Task ShowOrderList()
    {
        try
        {
            var orders = (await orderService.GetAllAsync())
                .OrderByDescending(o => o.OrderDatum)
                .ToList();
                
            if (!DataValidering.ValidateList(orders, "Inga ordrar hittades"))
            {
                Meny.Wait();
                return;
            }
            for (int i = 0; i < orders.Count; i++)
            {
                Console.WriteLine($"Id {i + 1}.{orders[i].OrderDatum:d} Kund: {orders[i].Kund.Namn} - Totalpris: {valutaSession.FormatPris(orders[i].TotalPris)} - Fraktombud: {orders[i].FraktOmbud.Namn} - Betalningsmetod: {orders[i].BetalningsMetod}");
                Meny.CreateLines('-', 120);
            }
            Meny.Wait();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
        }
    }

    
}
