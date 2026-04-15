using Webshop.Domain.Entitites;

namespace Webshop.Application.Interfaces;

public interface IProduktService
{
    Task<Produkt> GetByIdAsync(Guid id);
    Task<List<Produkt>> GetAllAsync();
    Task<Produkt> AddAsync(Produkt produkt);
    Task UpdateAsync(Produkt produkt);
    Task DeleteAsync(Guid id);
    Task<List<Produkt>> GetByKategoriAsync(Guid kategoriId);
    Task<List<Produkt>> GetByLeverantörAsync(Guid leverantörId);
    Task<List<Produkt>> SearchAsync(string SearchInput);
    Task<bool> ExistAsync(Guid id);
}