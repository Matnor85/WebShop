using Microsoft.EntityFrameworkCore;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.EF.Repositories;

public class KundRepository : IKundRepository
{
    private readonly WebshopDbContext _context;
    public KundRepository(WebshopDbContext context)
    {
        _context = context;
    }
    public async Task<Kund> AddAsync(Kund kund)
    {
        if (kund == null) return null;
        _context.Kunder.Add(kund);
        await _context.SaveChangesAsync();
        return kund;
    }

    public async Task<Kund> DeleteAsync(Guid id)
    {
        var kund = await _context.Kunder.FindAsync(id);
        if (kund == null) return null;

        _context.Kunder.Remove(kund);
        await _context.SaveChangesAsync();
        return kund;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Kunder.AnyAsync(k => k.Id == id);
    }

    public async Task<List<Kund>> GetAllAsync()
    {
        return await _context.Kunder.ToListAsync();
    }

    public async Task<Kund> GetByIdAsync(Guid id)
    {
        return await _context.Kunder.FindAsync(id);
    }

    public async Task<List<Kund>> SearchByNameAsync(string namn)
    {
        return await _context.Kunder
            .Where(k => k.Namn.Contains(namn))
            .ToListAsync();
    }

    public async Task<Kund> UpdateAsync(Kund kund)
    {
        _context.Kunder.Update(kund);
        await _context.SaveChangesAsync();
        return kund;
    }
}