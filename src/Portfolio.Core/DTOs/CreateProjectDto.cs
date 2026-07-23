namespace Portfolio.Core.DTOs;

public class CreateProjectDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? DynamicImpact { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime CompletedDate { get; set; }
    public List<int> TechnologyIds { get; set; } = new();
}

public class UpdateProjectDto : CreateProjectDto
{
    public int Id { get; set; }
}