using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.EF.Repositories;

public class LeverantörRepository : ILeverantörRepository
{
    public Task<Leverantör> AddAsync(Leverantör leverantör)
    {
        throw new NotImplementedException();
    }

    public Task<Leverantör> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Leverantör>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Leverantör> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Leverantör> UpdateAsync(Leverantör leverantör)
    {
        throw new NotImplementedException();
    }
}
