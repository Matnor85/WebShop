using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;

namespace Webshop.Application.Interfaces;

public interface ILeverantörService
{
    Task<Leverantör> GetByIdAsync(Guid id);
    Task<List<Leverantör>> GetAllAsync();
    Task<Leverantör> AddAsync(Leverantör leverantör);
    Task<Leverantör> DeleteAsync(Guid id);
    Task<Leverantör> UpdateAsync(Leverantör leverantör);
    Task<bool> ExistsAsync(Guid id);
}

