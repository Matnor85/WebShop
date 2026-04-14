using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.EF.Repositories;

public class OrderRepository : IOrderRepository
{
    WebshopDbContext _context;
    public OrderRepository(WebshopDbContext context)
    {
        _context = context;
    }

    public async Task<Order> AddAsync(Order order)
    {
        _context.Ordrar.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = _context.Ordrar.Find(id);
        if (order == null) return;
        _context.Ordrar.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Ordrar.AnyAsync(o => o.Id == id);
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Ordrar
            .Include(o => o.Kund)
            .Include(o => o.FraktOmbud)
            .Include(o => o.ProduktOrdrar)
            .ThenInclude(po => po.Produkt)
            .ToListAsync();
    }

    public Task<List<Order>> GetByDatumSpannAsync(DateTime från, DateTime till)
    {
        return _context.Ordrar.Where(o => o.OrderDatum >= från && o.OrderDatum <= till).ToListAsync();
    }

    public async Task<Order> GetByIdAsync(Guid id)
    {
        return await _context.Ordrar
            .Include(o => o.Kund)
            .Include(o => o.FraktOmbud)
            .Include(o => o.ProduktOrdrar)
            .ThenInclude(po => po.Produkt)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public Task<List<Order>> GetOrdersByFraktOmbudIdAsync(Guid fraktOmbudId)
    {
        return _context.Ordrar.Where(o => o.FraktOmbudId == fraktOmbudId).ToListAsync();
    }

    public Task<List<Order>> GetOrdersByKundIdAsync(Guid kundId)
    {
        return _context.Ordrar.Where(o => o.KundId == kundId).ToListAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Ordrar.Update(order);
        await _context.SaveChangesAsync();
    }
}
