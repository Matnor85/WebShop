using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.EF.Repositories;

public class ProduktRepository : IProduktRepository
{
    private readonly WebshopDbContext _context;

    public ProduktRepository( WebshopDbContext context)
    {
        _context = context;
    }
    public async Task<Produkt> AddAsync(Produkt produkt)
    {
        _context.Produkter.Add(produkt);
        await _context.SaveChangesAsync();
        return produkt;
    }

    public async Task DeleteAsync(Guid id)
    {
        var produkt = await _context.Produkter.FindAsync(id);
        if (produkt == null) return;

        _context.Produkter.Remove(produkt);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Produkter.AnyAsync(p => p.Id == id);
    }

    public async Task<List<Produkt>> GetAllAsync()
    {
        return await _context.Produkter.ToListAsync();
    }

    public async Task<Produkt> GetByIdAsync(Guid id)
    {
        return await _context.Produkter.FindAsync(id);
    }

    public async Task<List<Produkt>> GetByKategoriAsync(Guid KategoriId)
    {
        return await _context.Produkter.Where(p => p.KategoriId == KategoriId).ToListAsync();
    }

    public async Task<List<Produkt>> GetByLeverantörAsync(Guid LeverantörId)
    {
        return await _context.Produkter.Where(p => p.LeverantörId == LeverantörId).ToListAsync();
    }

    public async Task<List<Produkt>> SearchAsync(string searchInput)
    {
        if (string.IsNullOrWhiteSpace(searchInput)) {
            return new List<Produkt>();
        }

        return await _context.Produkter
            .Where(p => p.Namn.Contains(searchInput) || p.Beskrivning.Contains(searchInput))
            .ToListAsync();
    }

    public async Task UpdateAsync(Produkt produkt)
    {
       _context.Produkter.Update(produkt);
        await _context.SaveChangesAsync();
    }
   
}
