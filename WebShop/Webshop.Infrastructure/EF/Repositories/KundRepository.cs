using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.EF.Repositories;

internal class KundRepository : IKundRepository
{
    public Task<Kund> AddAsync(Kund kund)
    {
        throw new NotImplementedException();
    }

    public Task<Kund> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Kund>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Kund> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Kund>> SearchByNameAsync(string namn)
    {
        throw new NotImplementedException();
    }

    public Task<Kund> UpdateAsync(Kund kund)
    {
        throw new NotImplementedException();
    }
}
