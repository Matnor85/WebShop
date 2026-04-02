using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;

namespace Webshop.Domain.Interfaces;

public interface IKundRepository
{
    Task<Kund> GetByIdAsync(Guid id);
}
