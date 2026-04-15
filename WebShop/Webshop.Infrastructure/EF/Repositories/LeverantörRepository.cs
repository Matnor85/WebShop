using Microsoft.EntityFrameworkCore;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.EF.Repositories;

public class LeverantörRepository : ILeverantörRepository
{
    private readonly WebshopDbContext _context;

    public LeverantörRepository(WebshopDbContext context)
    {
        _context = context;
    }
    public async Task<Leverantör> AddAsync(Leverantör leverantör)
    {
        _context.Leverantörer.Add(leverantör);
        await _context.SaveChangesAsync();
        return leverantör;
    }

    public async Task<Leverantör> DeleteAsync(Guid id)
    {
        var leverantör = await _context.Leverantörer.FindAsync(id);
        if (leverantör == null) return null;

        _context.Leverantörer.Remove(leverantör);
        await _context.SaveChangesAsync();
        return leverantör;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Leverantörer.AnyAsync(l => l.Id == id);
    }

    public async Task<List<Leverantör>> GetAllAsync()
    {
        return await _context.Leverantörer.ToListAsync();
    }

    public async Task<Leverantör> GetByIdAsync(Guid id)
    {
        return await _context.Leverantörer.FindAsync(id);
    }

    public async Task<Leverantör> UpdateAsync(Leverantör leverantör)
    {
        _context.Leverantörer.Update(leverantör);
        await _context.SaveChangesAsync();
        return leverantör;
    }
}