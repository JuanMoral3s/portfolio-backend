namespace Portfolio.Core.Entities;

public class Experience
{
    public int Id { get; set; }
    public string CompanyOrLab { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string AchievementsSummary { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
