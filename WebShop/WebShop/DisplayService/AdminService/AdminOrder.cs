using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService.AdminService;

public class AdminOrder
{
    IOrderService _orderService;
    IKundService _kundService;
    public AdminOrder(IOrderService orderService, IKundService kundService)
    {
        _orderService = orderService;
        _kundService = kundService;
    }
    public async Task ShowOrderList()
    {
        try
        {
            var orders = await _orderService.GetAllAsync();
            if (!DataValidering.ValidateList(orders, "Inga ordrar hittades"))
            {
                Meny.Wait();
                return;
            }
            for (int i = 0; i < orders.Count; i++)
            {
                Console.WriteLine($"Id {i + 1}, Kund: {orders[i].Kund.Namn}, Totalpris: {orders[i].TotalPris}, Fraktombud: {orders[i].FraktOmbud.Namn}");
            }
            Meny.Wait();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
        }
    }

    
}
