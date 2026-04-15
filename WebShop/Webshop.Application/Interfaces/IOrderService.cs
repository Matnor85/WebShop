using Webshop.Domain.Entitites;

namespace Webshop.Application.Interfaces;

public interface IOrderService
{
    Task<Order> GetByIdAsync(Guid id);
    Task<List<Order>> GetAllAsync();
    Task<List<Order>> GetOrdersByKundIdAsync(Guid kundId);
    Task<List<Order>> GetOrdersByFraktOmbudIdAsync(Guid fraktOmbudId);
    Task<List<Order>> GetByDatumSpannAsync(DateTime från, DateTime till);
    Task<Order> AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}