using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Services;

public class FraktOmbudService(IFraktOmbudRepository _repository) : IFraktOmbudService
{
    public async Task<FraktOmbud> AddAsync(FraktOmbud fraktOmbud) => await _repository.AddAsync(fraktOmbud);
    public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    public async Task<List<FraktOmbud>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<FraktOmbud> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
    public async Task UpdateAsync(FraktOmbud fraktOmbud) => await _repository.UpdateAsync(fraktOmbud);
    public async Task<bool> ExistsAsync(Guid id) => await _repository.ExistsAsync(id);
}