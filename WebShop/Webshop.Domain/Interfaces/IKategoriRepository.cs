using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;

namespace Webshop.Domain.Interfaces;

public interface IKategoriRepository
{
    Task<Kategori> GetByIdAsync(Guid id);
    Task<List<Kategori>> GetAllAsync();
    Task<Kategori> AddAsync(Kategori kategori);
    Task UpDateAsync(Kategori kategori);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);

}
