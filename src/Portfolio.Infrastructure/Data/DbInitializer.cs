using Portfolio.Core.Entities;

namespace Portfolio.Infrastructure.Data;

public static class DbInitializer
{
    public static void Seed(PortfolioDbContext context)
    {
        // Asegurar que la base de datos existe
        context.Database.EnsureCreated();

        // Si ya hay proyectos, no hacemos nada para evitar duplicar
        if (context.Projects.Any())
        {
            return;
        }

        // 1. Tecnologías Base
        var csharp = new Technology { Name = "C#", Category = "Backend" };
        var net = new Technology { Name = ".NET", Category = "Framework" };
        var swift = new Technology { Name = "Swift / SwiftUI", Category = "Mobile" };
        var sql = new Technology { Name = "SQL Server", Category = "Database" };
        var ai = new Technology { Name = "Core ML / AI Agents", Category = "Artificial Intelligence" };

        context.Technologies.AddRange(csharp, net, swift, sql, ai);
        context.SaveChanges();

        // 2. Proyectos y Hackathons
        var projects = new[]
        {
            new Project
            {
                Title = "Puerta Liverpool",
                Description = "Aplicación móvil omnichannel de retail enfocada en mejorar la experiencia del cliente.",
                DynamicImpact = "1er Lugar - Hackathon Nacional iOS",
                Type = "Mobile / Hackathon",
                CompletedDate = new DateTime(2026, 5, 1),
                ProjectTechnologies = new List<ProjectTechnology>
                {
                    new ProjectTechnology { Technology = swift },
                    new ProjectTechnology { Technology = csharp }
                }
            },
            new Project
            {
                Title = "UniWay",
                Description = "Aplicación móvil de accesibilidad e inclusión universitaria.",
                DynamicImpact = "1er Lugar Categoría Incluyente - Hackathon UNAM",
                Type = "Mobile / Accessibility",
                CompletedDate = new DateTime(2026, 4, 1),
                ProjectTechnologies = new List<ProjectTechnology>
                {
                    new ProjectTechnology { Technology = swift }
                }
            },
            new Project
            {
                Title = "Regalo B.I.",
                Description = "Plataforma empresarial orientada a la retención y fidelización de empleados.",
                DynamicImpact = "2do Lugar - Hackathon Nacional",
                Type = "Enterprise / Web",
                CompletedDate = new DateTime(2026, 6, 1),
                ProjectTechnologies = new List<ProjectTechnology>
                {
                    new ProjectTechnology { Technology = csharp },
                    new ProjectTechnology { Technology = net },
                    new ProjectTechnology { Technology = sql }
                }
            }
        };

        context.Projects.AddRange(projects);

        // 3. Experiencia Laboral
        var experiences = new[]
        {
            new Experience
            {
                CompanyOrLab = "Danone",
                Role = "Backend Developer Intern",
                AchievementsSummary = "Desarrollo y optimización de servicios de backend corporativos.",
                StartDate = new DateTime(2026, 1, 1),
                EndDate = null // Empleo activo
            },
            new Experience
            {
                CompanyOrLab = "Laboratorio de Desarrollo iOS - UNAM",
                Role = "iOS Developer & Mentor",
                AchievementsSummary = "Mentoría a estudiantes y desarrollo de aplicaciones móviles con ecosistema Apple.",
                StartDate = new DateTime(2025, 8, 1),
                EndDate = null
            }
        };

        context.Experiences.AddRange(experiences);
        context.SaveChanges();
    }
}