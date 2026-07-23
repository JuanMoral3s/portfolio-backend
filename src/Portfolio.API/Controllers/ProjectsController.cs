using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Portfolio.API.Mappers;
using Portfolio.Core.DTOs;
using Portfolio.Core.Interfaces;

namespace Portfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectRepository _repository;
    private readonly IStringLocalizer<ProjectsController> _localizer;

    public ProjectsController(
        IProjectRepository repository, 
        IStringLocalizer<ProjectsController> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll()
    {
        var projects = await _repository.GetAllAsync();
        var dtos = projects.Select(p => p.ToDto());
        return Ok(dtos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectDto>> GetById(int id)
    {
        var project = await _repository.GetByIdAsync(id);
        if (project == null)
        {
            string errorMessage = string.Format(_localizer["ProjectNotFound"], id);

            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = errorMessage
            });
        }

        return Ok(project.ToDto());
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ProjectDto>> Create(CreateProjectDto dto)
    {
        var project = dto.ToEntity();
        await _repository.AddAsync(project);

        if (await _repository.SaveChangesAsync())
        {
            var createdProject = await _repository.GetByIdAsync(project.Id);
            return CreatedAtAction(nameof(GetById), new { id = project.Id }, createdProject?.ToDto() ?? project.ToDto());
        }

        return BadRequest(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Detail = "No se pudo crear el proyecto."
        });
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, UpdateProjectDto dto)
    {
        var project = await _repository.GetByIdAsync(id);
        if (project == null)
        {
            string errorMessage = string.Format(_localizer["ProjectNotFound"], id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = errorMessage
            });
        }

        project.Title = dto.Title;
        project.Description = dto.Description;
        project.DynamicImpact = dto.DynamicImpact;
        project.Type = dto.Type;
        project.CompletedDate = dto.CompletedDate;

        project.ProjectTechnologies = dto.TechnologyIds.Select(techId => new Portfolio.Core.Entities.ProjectTechnology
        {
            ProjectId = id,
            TechnologyId = techId
        }).ToList();

        _repository.Update(project);

        if (await _repository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Detail = "No se pudo actualizar el proyecto."
        });
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _repository.GetByIdAsync(id);
        if (project == null)
        {
            string errorMessage = string.Format(_localizer["ProjectNotFound"], id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = errorMessage
            });
        }

        _repository.Delete(project);

        if (await _repository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Detail = "No se pudo eliminar el proyecto."
        });
    }
}