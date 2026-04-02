using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;

namespace Webshop.Domain.Interfaces;

public interface IProduktRepository
{
    Task<Produkt> GetByIdAsync(Guid id);
    Task<List<Produkt>> GetAllAsync();
    Task<List<Produkt>> GetByKategoriAsync(Guid KategoriId);
    Task<List<Produkt>> GetByLeverantörAsync(Guid LeverantörId);
    Task<List<Produkt>> SearchAsync(string searchTerm);
    Task<Produkt> AddAsync(Produkt produkt);
    Task UpdateAsync(Produkt produkt);
    Task DeleteAsync(Guid id);
    Task UpdateLagerAntalAsync(Guid id, int nyttAntal);
    Task<bool> ExistsAsync(Guid id);
    
}
