using Webshop.Domain.Entitites;

namespace Webshop.Domain.Interfaces;

public interface IProduktKampanjRepository
{
    Task<List<ProduktKampanj>> GetAllAsync();
    Task<ProduktKampanj> AddAsync(ProduktKampanj produktKampanj);
    Task UpdateAsync(ProduktKampanj produktKampanj);
    Task DeleteAsync(Guid id);
    Task<bool> ExistAsync(Guid id);
    Task<ProduktKampanj> GetByIdAsync(Guid id);
}