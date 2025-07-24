using LighthouseSocial.Domain.Entities;

namespace LighthouseSocial.Domain.Interfaces;

public interface ILighthouseRepository
{
    Task<Lighthouse?> GetByIdAsync(Guid id);
    Task<IEnumerable<Lighthouse>> GetAllAsync();

    Task AddAsync(Lighthouse lighthouse);
    Task Update(Lighthouse lighthouse);
    Task Delete(Guid id);
}