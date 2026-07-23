using Portfolio.Core.Entities;

namespace Portfolio.Core.Interfaces;

public interface IExperienceRepository
{
    Task<IEnumerable<Experience>> GetAllAsync();
    Task<Experience?> GetByIdAsync(int id);
    Task AddAsync(Experience experience);
    void Update(Experience experience);
    void Delete(Experience experience);
    Task<bool> SaveChangesAsync();
}