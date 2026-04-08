using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Services;

public class OrderService(IOrderRepository _repository) : IOrderService
{
    public async Task<Order> AddAsync(Order order) => await _repository.AddAsync(order);
    public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    public async Task<bool> ExistsAsync(Guid id) => await _repository.ExistsAsync(id);
    public async Task<List<Order>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<List<Order>> GetByDatumSpannAsync(DateTime från, DateTime till) => await _repository.GetByDatumSpannAsync(från, till);
    public async Task<Order> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
    public async Task<List<Order>> GetOrdersByFraktOmbudIdAsync(Guid fraktOmbudId) => await _repository.GetOrdersByFraktOmbudIdAsync(fraktOmbudId);
    public async Task<List<Order>> GetOrdersByKundIdAsync(Guid kundId) => await _repository.GetOrdersByKundIdAsync(kundId);
    public async Task UpdateAsync(Order order) => await _repository.UpdateAsync(order);
}
