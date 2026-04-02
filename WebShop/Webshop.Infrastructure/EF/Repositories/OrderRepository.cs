using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.EF.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<Order> AddOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<Order> DeleteOrder(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Order> FindByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetByDatumSpannAsync(DateTime från, DateTime till)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetOrdersByFraktOmbudIdAsync(Guid fraktOmbudId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetOrdersByKundIdAsync(Guid kundId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> OrderExists(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Order> UpdateOrder(Order order)
    {
        throw new NotImplementedException();
    }
}
