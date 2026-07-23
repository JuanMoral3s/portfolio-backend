using Portfolio.Core.DTOs;
using Portfolio.Core.Entities;

namespace Portfolio.API.Mappers;

public static class MappingExtensions
{
    // Mapeos de lectura (Salida)
    public static ProjectDto ToDto(this Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Title = project.Title ?? string.Empty,
            Description = project.Description ?? string.Empty,
            DynamicImpact = project.DynamicImpact ?? string.Empty,
            Type = project.Type ?? string.Empty,
            CompletedDate = project.CompletedDate,
            Technologies = project.ProjectTechnologies
                .Where(pt => pt.Technology != null)
                .Select(pt => pt.Technology!.Name ?? string.Empty)
                .ToList()
        };
    }

    public static ExperienceDto ToDto(this Experience experience)
    {
        return new ExperienceDto
        {
            Id = experience.Id,
            CompanyOrLab = experience.CompanyOrLab ?? string.Empty,
            Role = experience.Role ?? string.Empty,
            AchievementsSummary = experience.AchievementsSummary ?? string.Empty,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate
        };
    }

    // Mapeos de creación/actualización (Entrada)
    public static Experience ToEntity(this CreateExperienceDto dto)
    {
        return new Experience
        {
            CompanyOrLab = dto.CompanyOrLab,
            Role = dto.Role,
            AchievementsSummary = dto.AchievementsSummary,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate
        };
    }

    public static Project ToEntity(this CreateProjectDto dto)
    {
        return new Project
        {
            Title = dto.Title,
            Description = dto.Description,
            DynamicImpact = dto.DynamicImpact,
            Type = dto.Type,
            CompletedDate = dto.CompletedDate,
            ProjectTechnologies = dto.TechnologyIds.Select(techId => new ProjectTechnology
            {
                TechnologyId = techId
            }).ToList()
        };
    }
}