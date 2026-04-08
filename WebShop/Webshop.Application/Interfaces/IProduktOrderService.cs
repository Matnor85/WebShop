using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;

namespace Webshop.Application.Interfaces;

public interface IProduktOrderService
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
