namespace Portfolio.Core.DTOs;

public class ProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DynamicImpact { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime CompletedDate { get; set; }
    public List<string> Technologies { get; set; } = new();
}