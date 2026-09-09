namespace WhoIAm.Application.DTOs.Mission;

public class CreateMissionRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int EstimatedMinutes { get; set; }
    public string Difficulty { get; set; } = "Easy"; // Easy, Medium, Hard
    public DateTime? DueDate { get; set; }
}

public class MissionResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int EstimatedMinutes { get; set; }
    public string Difficulty { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
