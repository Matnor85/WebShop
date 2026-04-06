using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.EF.Repositories;

public class ProduktOrderRepository : IProduktOrderRepository
{
   private readonly WebshopDbContext _context;
    public ProduktOrderRepository(WebshopDbContext context)
    {
        _context = context;
    }

    public async Task<ProduktOrder> AddAsync(ProduktOrder produktOrder)
    {
        _context.ProduktOrdrar.Add(produktOrder);
        await _context.SaveChangesAsync();
        return produktOrder;
    }

    public async Task DeleteAsync(Guid id)
    {
        var produktOrder = await _context.ProduktOrdrar.FindAsync(id);
        if (produktOrder == null) return;
        _context.ProduktOrdrar.Remove(produktOrder);
        await _context.SaveChangesAsync();

    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.ProduktOrdrar.AnyAsync(po => po.Id == id);
    }

    public async Task<List<ProduktOrder>> GetAllAsync()
    {
        return await _context.ProduktOrdrar.ToListAsync();
    }

    public async Task<ProduktOrder> GetByIdAsync(Guid id)
    {
        return await _context.ProduktOrdrar.FindAsync(id);
    }

    public async Task<List<ProduktOrder>> GetByOrder(Guid orderId)
    {
        return await _context.ProduktOrdrar.Where(po => po.OrderId == orderId).ToListAsync();
    }

    public async Task<List<ProduktOrder>> GetByProdukt(Guid produktId)
    {
        return await _context.ProduktOrdrar.Where(po => po.ProduktId == produktId).ToListAsync();
    }

    public async Task UpdateAsync(ProduktOrder produktOrder)
    {
        _context.ProduktOrdrar.Update(produktOrder);
        await _context.SaveChangesAsync();
    }
}
