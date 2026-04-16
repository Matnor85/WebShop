using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Services;

public class ProduktOrderService(IProduktOrderRepository _repository) : IProduktOrderService
{
    public async Task<ProduktOrder> AddAsync(ProduktOrder produktOrder) => await _repository.AddAsync(produktOrder);
    public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    public async Task<bool> ExistsAsync(Guid id) => await _repository.ExistsAsync(id);
    public async Task<List<ProduktOrder>> GetAllAsync() => await _repository.GetAllAsync();
    public Task<ProduktOrder> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);
    public async Task<List<ProduktOrder>> GetByOrder(Guid orderId) => await _repository.GetByOrder(orderId);
    public async Task<List<ProduktOrder>> GetByProdukt(Guid produktId) => await _repository.GetByProdukt(produktId);
    public async Task UpdateAsync(ProduktOrder produktOrder) => await _repository.UpdateAsync(produktOrder);
}