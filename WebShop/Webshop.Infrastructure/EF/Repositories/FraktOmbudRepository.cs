using Microsoft.EntityFrameworkCore;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.EF.Repositories;

public class FraktOmbudRepository : IFraktOmbudRepository
{
    private readonly WebshopDbContext _context;
    public FraktOmbudRepository(WebshopDbContext context)
    {
        _context = context;
    }
    public async Task<FraktOmbud> AddAsync(FraktOmbud fraktOmbud)
    {
        _context.FraktOmbud.Add(fraktOmbud);
        await _context.SaveChangesAsync();
        return fraktOmbud;
    }

    public async Task DeleteAsync(Guid id)
    {
        var fraktOmbud = await _context.FraktOmbud.FindAsync(id);
        if (fraktOmbud == null) return; 
        
            _context.FraktOmbud.Remove(fraktOmbud);
            await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.FraktOmbud.AnyAsync(f => f.Id == id);
    }

    public async Task<List<FraktOmbud>> GetAllAsync()
    {
        return await _context.FraktOmbud.ToListAsync();
    }

    public async Task<FraktOmbud> GetByIdAsync(Guid id)
    {
        return await _context.FraktOmbud.FindAsync(id);
    }

    public async Task UpdateAsync(FraktOmbud fraktOmbud)
    {
        _context.FraktOmbud.Update(fraktOmbud);
        await _context.SaveChangesAsync();
    }
}