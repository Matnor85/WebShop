using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.EF.Repositories;

class FraktOmbudRepository : IFraktOmbudRepository
{
    public Task<FraktOmbud> AddAsync(FraktOmbud fraktOmbud)
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

    public Task<List<FraktOmbud>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<FraktOmbud> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(FraktOmbud fraktOmbud)
    {
        throw new NotImplementedException();
    }
}
