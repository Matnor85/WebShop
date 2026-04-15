using Webshop.Domain.Entitites;

namespace Webshop.Application.Interfaces;

public interface IKundService
{
    Task<Kund> GetByIdAsync(Guid id);
    Task<List<Kund>> GetAllAsync();
    Task<List<Kund>> SearchByNameAsync(string namn);
    Task<Kund> AddAsync(Kund kund);
    Task<Kund> UpdateAsync(Kund kund);
    Task<Kund> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}