using Webshop.Domain.Entitites;

namespace Webshop.Application.Interfaces;

public interface IFraktOmbudService
{
    Task<FraktOmbud> GetByIdAsync(Guid id);
    Task<List<FraktOmbud>> GetAllAsync();
    Task<FraktOmbud> AddAsync(FraktOmbud fraktOmbud);
    Task UpdateAsync(FraktOmbud fraktOmbud);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}