using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.EF.Repositories;

public class ProduktKampanjRepository(WebshopDbContext context) : IProduktKampanjRepository
{
    public async Task<ProduktKampanj> AddAsync(ProduktKampanj produktKampanj)
    {
        context.ProduktKampanjer.Add(produktKampanj);
        await context.SaveChangesAsync();
        return produktKampanj;
    }

    public async Task DeleteAsync(Guid id)
    {
        var kampanj = await context.ProduktKampanjer.FindAsync(id);
        if (kampanj == null) return;
        context.ProduktKampanjer.Remove(kampanj);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        return await context.ProduktKampanjer.AnyAsync(pk => pk.Id == id);
    }

    public async Task<List<ProduktKampanj>> GetAllAsync()
    {
        return await context.ProduktKampanjer
            .Include(pk => pk.Produkt)
            .ToListAsync();
    }

    public async Task<ProduktKampanj> GetByIdAsync(Guid id)
    {
        return await context.ProduktKampanjer
            .Include(pk => pk.Produkt)
            .FirstOrDefaultAsync(pk => pk.Id == id);
    }

    public async Task UpdateAsync(ProduktKampanj produktKampanj)
    {
        context.ProduktKampanjer.Update(produktKampanj);
        await context.SaveChangesAsync();
    }
}
