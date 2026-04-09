using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;

namespace WebShop.Presentation.DisplayService;

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
            if (orders == null)
                throw new ArgumentException("Inga order hittades.");
            for (int i = 0; i < orders.Count; i++)
            {
                Console.WriteLine($"Id {i + 1}, Kund: {orders[i].Kund.Namn}, Totalpris: {orders[i].TotalPris}, Fraktombud: {orders[i].FraktOmbud.Namn}");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Fel: {ex.Message}");
        }
    }

    
}
