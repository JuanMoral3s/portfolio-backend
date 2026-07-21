using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Entities;

namespace Portfolio.Infrastructure.Data;

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Technology> Technologies { get; set; }
    public DbSet<ProjectTechnology> ProjectTechnologies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la llave compuesta para la relación Muchos a Muchos
        modelBuilder.Entity<ProjectTechnology>()
            .HasKey(pt => new { pt.ProjectId, pt.TechnologyId });

        modelBuilder.Entity<ProjectTechnology>()
            .HasOne(pt => pt.Project)
            .WithMany(p => p.ProjectTechnologies)
            .HasForeignKey(pt => pt.ProjectId);

        modelBuilder.Entity<ProjectTechnology>()
            .HasOne(pt => pt.Technology)
            .WithMany(t => t.ProjectTechnologies)
            .HasForeignKey(pt => pt.TechnologyId);

        // Configuraciones adicionales y restricciones limpias
        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(p => p.Title).IsRequired().HasMaxLength(150);
            entity.Property(p => p.Type).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Technology>(entity =>
        {
            entity.Property(t => t.Name).IsRequired().HasMaxLength(50);
            entity.Property(t => t.Category).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.Property(e => e.CompanyOrLab).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Role).IsRequired().HasMaxLength(100);
        });
    }
}
