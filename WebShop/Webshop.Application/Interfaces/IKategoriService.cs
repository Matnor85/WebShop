using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;

namespace Webshop.Application.Interfaces;

public interface IKategoriService
{
    Task<Kategori> GetByIdAsync(Guid id);
    Task<List<Kategori>> GetAllAsync();
    Task<Kategori> AddAsync(Kategori kategori);
    Task UpdateAsync(Kategori kategori);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
