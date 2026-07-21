namespace Portfolio.Core.Entities;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? DynamicImpact { get; set; }
    public DateTime CompletedDate { get; set; }
    public string Type { get; set; } = string.Empty;
    public ICollection<ProjectTechnology> ProjectTechnologies { get; set; } = [];
}
