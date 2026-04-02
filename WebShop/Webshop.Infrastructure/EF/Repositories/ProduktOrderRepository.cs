using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.EF.Repositories;

internal class ProduktOrderRepository : IProduktRepository
{
    public Task<Produkt> AddAsync(Produkt produkt)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Produkt>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Produkt> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Produkt>> GetByKategoriAsync(Guid KategoriId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Produkt>> GetByLeverantörAsync(Guid LeverantörId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Produkt>> SearchAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Produkt produkt)
    {
        throw new NotImplementedException();
    }

    public Task UpdateLagerAntalAsync(Guid id, int nyttAntal)
    {
        throw new NotImplementedException();
    }
}
