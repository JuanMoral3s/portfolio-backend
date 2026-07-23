using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Entities;
using Portfolio.Core.Interfaces;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly PortfolioDbContext _context;

    public ProjectRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _context.Set<Project>()
            .Include(p => p.ProjectTechnologies)
                .ThenInclude(pt => pt.Technology)
            .ToListAsync();
    }

    public async Task<(IEnumerable<Project> Items, int TotalCount)> GetPagedAsync(
        int pageNumber, int pageSize, string? technology = null, string? type = null)
    {
        var query = _context.Set<Project>()
            .Include(p => p.ProjectTechnologies)
                .ThenInclude(pt => pt.Technology)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(type))
        {
            query = query.Where(p => p.Type.ToLower() == type.ToLower());
        }

        if (!string.IsNullOrWhiteSpace(technology))
        {
            query = query.Where(p => p.ProjectTechnologies
                .Any(pt => pt.Technology != null && pt.Technology.Name.ToLower() == technology.ToLower()));
        }

        int totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _context.Set<Project>()
            .Include(p => p.ProjectTechnologies)
                .ThenInclude(pt => pt.Technology)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Project project)
    {
        await _context.Set<Project>().AddAsync(project);
    }

    public void Update(Project project)
    {
        _context.Set<Project>().Update(project);
    }

    public void Delete(Project project)
    {
        _context.Set<Project>().Remove(project);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}