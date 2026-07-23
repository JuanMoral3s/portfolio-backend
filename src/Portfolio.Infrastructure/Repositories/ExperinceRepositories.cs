using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Entities;
using Portfolio.Core.Interfaces;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories;

public class ExperienceRepository : IExperienceRepository
{
    private readonly PortfolioDbContext _context;

    public ExperienceRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Experience>> GetAllAsync()
    {
        return await _context.Set<Experience>().ToListAsync();
    }

    public async Task<Experience?> GetByIdAsync(int id)
    {
        return await _context.Set<Experience>().FindAsync(id);
    }

    public async Task AddAsync(Experience experience)
    {
        await _context.Set<Experience>().AddAsync(experience);
    }

    public void Update(Experience experience)
    {
        _context.Set<Experience>().Update(experience);
    }

    public void Delete(Experience experience)
    {
        _context.Set<Experience>().Remove(experience);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}