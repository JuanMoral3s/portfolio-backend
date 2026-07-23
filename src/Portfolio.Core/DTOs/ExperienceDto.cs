namespace Portfolio.Core.DTOs;

public class ExperienceDto
{
    public int Id { get; set; }
    public string CompanyOrLab { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string AchievementsSummary { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrent => EndDate == null;
}