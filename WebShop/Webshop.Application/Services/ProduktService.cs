using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Services;

public class ProduktService(IProduktRepository _repository): IProduktService 
{
    public async Task<Produkt> AddAsync(Produkt produkt) => await _repository.AddAsync(produkt);
    public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    public async Task<List<Produkt>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Produkt> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
    public async Task<List<Produkt>> GetByKategoriAsync(Guid kategoriId) => await _repository.GetByKategoriAsync(kategoriId);
    public async Task<List<Produkt>> GetByLeverantörAsync(Guid leverantörId) => await _repository.GetByLeverantörAsync(leverantörId);
    public async Task UpdateAsync(Produkt produkt) => await _repository.UpdateAsync(produkt);
    public async Task<List<Produkt>> ProduktSearch(string SearchInput) => await _repository.SearchAsync(SearchInput);
    public async Task<bool> ExistsAsync(Guid id) => await _repository.ExistsAsync(id);

}
