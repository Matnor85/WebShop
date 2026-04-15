using Webshop.Domain.Entitites;

namespace Webshop.Domain.Interfaces;

public interface IProduktOrderRepository
{
    Task<ProduktOrder> GetByIdAsync(Guid id);
    Task<List<ProduktOrder>> GetAllAsync();
    Task<List<ProduktOrder>> GetByOrder(Guid orderId);
    Task<List<ProduktOrder>> GetByProdukt(Guid produktId);
    Task<ProduktOrder> AddAsync(ProduktOrder produktOrder);
    Task UpdateAsync(ProduktOrder produktOrder);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}