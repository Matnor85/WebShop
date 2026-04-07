using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Services;

public class LeverantörService(ILeverantörRepository _repository) : ILeverantörService
{
    public async Task<Leverantör> AddAsync(Leverantör leverantör) => await _repository.AddAsync(leverantör);
    public async Task<Leverantör> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    public Task<bool> ExistsAsync(Guid id) => _repository.ExistsAsync(id);
    public async Task<List<Leverantör>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Leverantör> GetByIdAsync(Guid id) => await GetByIdAsync(id);
    public async Task<Leverantör> UpdateAsync(Leverantör leverantör) => await _repository.UpdateAsync(leverantör);
}
