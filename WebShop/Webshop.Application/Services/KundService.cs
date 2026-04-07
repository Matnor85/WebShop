using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Services;

public class KundService(IKundRepository _repository) : IKundService
{
    public async Task<Kund> AddAsync(Kund kund) => await _repository.AddAsync(kund);
    public async Task<Kund> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    public async Task<bool> ExistsAsync(Guid id) => await _repository.ExistsAsync(id);
    public async Task<List<Kund>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Kund> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
    public async Task<List<Kund>> SearchByNameAsync(string namn) => await _repository.SearchByNameAsync(namn);
    public async Task<Kund> UpdateAsync(Kund kund) => await _repository.UpdateAsync(kund);
}