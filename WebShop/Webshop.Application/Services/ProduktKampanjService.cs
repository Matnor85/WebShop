using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Services;

public class ProduktKampanjService(IProduktKampanjRepository Repository) : IProduktKampanjService
{
    public async Task<ProduktKampanj> AddAsync(ProduktKampanj produktKampanj) => await Repository.AddAsync(produktKampanj);
    public async Task DeleteAsync(Guid id) => await Repository.DeleteAsync(id);
    public async Task<bool> ExistAsync(Guid id) => await Repository.ExistAsync(id);
    public async Task<List<ProduktKampanj>> GetAllAsync() => await Repository.GetAllAsync();
    public async Task<ProduktKampanj> GetByIdAsync(Guid id) => await Repository.GetByIdAsync(id);
    public async Task UpdateAsync(ProduktKampanj produktKampanj) => await Repository.UpdateAsync(produktKampanj);
}
