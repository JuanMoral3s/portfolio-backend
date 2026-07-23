using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Portfolio.API.Mappers;
using Portfolio.Core.DTOs;
using Portfolio.Core.Interfaces;

namespace Portfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExperiencesController : ControllerBase
{
    private readonly IExperienceRepository _repository;
    private readonly IStringLocalizer<ExperiencesController> _localizer;

    public ExperiencesController(
        IExperienceRepository repository, 
        IStringLocalizer<ExperiencesController> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExperienceDto>>> GetAll()
    {
        var experiences = await _repository.GetAllAsync();
        var dtos = experiences.Select(e => e.ToDto());
        return Ok(dtos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ExperienceDto>> GetById(int id)
    {
        var experience = await _repository.GetByIdAsync(id);
        if (experience == null)
        {
            string errorMessage = string.Format(_localizer["ExperienceNotFound"], id);

            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = errorMessage
            });
        }

        return Ok(experience.ToDto());
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ExperienceDto>> Create(CreateExperienceDto createDto)
    {
        var experience = createDto.ToEntity();

        await _repository.AddAsync(experience);
        if (await _repository.SaveChangesAsync())
        {
            return CreatedAtAction(nameof(GetById), new { id = experience.Id }, experience.ToDto());
        }

        return BadRequest(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Detail = "No se pudo guardar la experiencia."
        });
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, UpdateExperienceDto updateDto)
    {
        var experience = await _repository.GetByIdAsync(id);
        if (experience == null)
        {
            string errorMessage = string.Format(_localizer["ExperienceNotFound"], id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = errorMessage
            });
        }

        experience.CompanyOrLab = updateDto.CompanyOrLab;
        experience.Role = updateDto.Role;
        experience.AchievementsSummary = updateDto.AchievementsSummary;
        experience.StartDate = updateDto.StartDate;
        experience.EndDate = updateDto.EndDate;

        _repository.Update(experience);
        
        if (await _repository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Detail = "No se pudo actualizar la experiencia."
        });
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var experience = await _repository.GetByIdAsync(id);
        if (experience == null)
        {
            string errorMessage = string.Format(_localizer["ExperienceNotFound"], id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = errorMessage
            });
        }

        _repository.Delete(experience);
        
        if (await _repository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Detail = "No se pudo eliminar la experiencia."
        });
    }
}