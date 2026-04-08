using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Services;

public class KategoriService(IKategoriRepository _repository) : IKategoriService
{
    public async Task<Kategori> AddAsync(Kategori kategori) => await _repository.AddAsync(kategori);
    public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    public async Task<bool> ExistsAsync(Guid id) => await _repository.ExistsAsync(id);
    public async Task<List<Kategori>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Kategori> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
    public async Task UpdateAsync(Kategori kategori) => await _repository.UpdateAsync(kategori);
}
