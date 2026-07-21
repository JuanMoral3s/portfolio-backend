namespace Portfolio.Core.Entities;

public class Technology
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public ICollection<ProjectTechnology> ProjectTechnologies { get; set; } = [];
}
