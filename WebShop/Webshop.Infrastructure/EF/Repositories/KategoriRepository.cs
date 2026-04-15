using Microsoft.EntityFrameworkCore;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.EF.Repositories;

public class KategoriRepository : IKategoriRepository
{
    private readonly WebshopDbContext _context;
    public KategoriRepository(WebshopDbContext context)
    {
        _context = context;
    }
    public async Task<Kategori> AddAsync(Kategori kategori)
    {
        _context.Kategorier.Add(kategori);
        await _context.SaveChangesAsync();
        return kategori;
    }

    public async Task DeleteAsync(Guid id)
    {
        var kategori = await _context.Kategorier.FindAsync(id);
        if (kategori == null) return;

        _context.Kategorier.Remove(kategori);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Kategorier.AnyAsync(k => k.Id == id);
    }

    public async Task<List<Kategori>> GetAllAsync()
    {
        return await _context.Kategorier
            .Include(p => p.Produkter)
            .ToListAsync();
    }

    public async Task<Kategori> GetByIdAsync(Guid id)
    {
        return await _context.Kategorier.FindAsync(id);
    }

    public async Task UpdateAsync(Kategori kategori)
    {
        _context.Kategorier.Update(kategori);
        await _context.SaveChangesAsync(); 
    }
}