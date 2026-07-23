using Portfolio.Core.Entities;

namespace Portfolio.Core.Interfaces;

public interface IProjectRepository
{
    // Métodos de lectura existentes
    Task<IEnumerable<Project>> GetAllAsync();
    Task<(IEnumerable<Project> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, string? technology = null, string? type = null);
    Task<Project?> GetByIdAsync(int id);

    // Nuevos contratos para el CRUD
    Task AddAsync(Project project);
    void Update(Project project);
    void Delete(Project project);
    Task<bool> SaveChangesAsync();
};