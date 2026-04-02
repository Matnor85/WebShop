using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;

namespace Webshop.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid id);
    Task<List<Order>> GetAllAsync();
    Task<List<Order>> GetOrdersByKundIdAsync(Guid kundId);
    Task<List<Order>> GetOrdersByFraktOmbudIdAsync(Guid fraktOmbudId);
    Task<List<Order>> GetByDatumSpannAsync(DateTime från, DateTime till);
    Task<Order> FindByIdAsync(Guid id);
    Task<Order> AddOrder(Order order);
    Task<Order> UpdateOrder(Order order);
    Task<Order> DeleteOrder(Guid id);
    Task<bool> OrderExists(Guid id);

}
