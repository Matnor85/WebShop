using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;

namespace Webshop.Application.Interfaces;

public interface IProduktService
{
    Task<Produkt> GetByIdAsync(Guid id);
    Task<List<Produkt>> GetAllAsync();
    Task<Produkt> AddAsync(Produkt produkt);
    Task UpdateAsync(Produkt produkt);
    Task DeleteAsync(Guid id);
}
