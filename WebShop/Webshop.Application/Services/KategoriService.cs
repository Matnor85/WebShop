using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Services;

public class KategoriService : IKategoriService 
{
    public IKategoriRepository _repository;
    public IProduktService _produktService;
    public KategoriService(IKategoriRepository repository, IProduktService produktService)
    {
         _repository = repository;
         _produktService = produktService;
    }
    public async Task<Kategori> AddAsync(Kategori kategori) => await _repository.AddAsync(kategori);
    public async Task DeleteAsync(Guid id)
    {
        var produkter = await _produktService.GetByKategoriAsync(id);
        if (produkter.Count > 0)
            Console.WriteLine($"Kan inte ta bort kategorin, {produkter.Count} produkter tillhör kategorin");
        else
            await _repository.DeleteAsync(id);
    }
    public async Task<bool> ExistsAsync(Guid id) => await _repository.ExistsAsync(id);
    public async Task<List<Kategori>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Kategori> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
    public async Task UpdateAsync(Kategori kategori) => await _repository.UpdateAsync(kategori);
}
