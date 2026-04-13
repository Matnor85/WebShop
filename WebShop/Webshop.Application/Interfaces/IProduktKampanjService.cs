using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;

namespace Webshop.Application.Interfaces;

public interface IProduktKampanjService
{
    Task<ProduktKampanj> GetByIdAsync(Guid id);
    Task<List<ProduktKampanj>> GetAllAsync();
    Task<ProduktKampanj> AddAsync(ProduktKampanj produktKampanj);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(ProduktKampanj produktKampanj);
    Task<bool> ExistAsync(Guid id);
}
