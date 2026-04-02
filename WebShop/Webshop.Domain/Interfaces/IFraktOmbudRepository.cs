using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;

namespace Webshop.Domain.Interfaces;

public interface IFraktOmbudRepository
{
    Task<FraktOmbud> GetByIdAsync(Guid id);
    Task<List<FraktOmbud>> GetAllAsync();
    Task<FraktOmbud> AddAsync(FraktOmbud fraktOmbud);
    Task UpdateAsync(FraktOmbud fraktOmbud);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
