using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.EF.Repositories;

public class KategoriRepository : IKategoriRepository
{
    public Task<Kategori> AddAsync(Kategori kategori)
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

    public Task<List<Kategori>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Kategori> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpDateAsync(Kategori kategori)
    {
        throw new NotImplementedException();
    }
}
