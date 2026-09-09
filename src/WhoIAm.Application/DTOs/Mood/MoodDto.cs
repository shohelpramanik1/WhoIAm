namespace WhoIAm.Application.DTOs.Mood;

public class CreateMoodRequest
{
    public string Mood { get; set; } = string.Empty; // Happy, Good, Neutral, Sad, Angry, etc.
    public int Intensity { get; set; } = 5; // 1-10
    public string? Note { get; set; }
}

public class MoodResponse
{
    public Guid Id { get; set; }
    public string Mood { get; set; } = string.Empty;
    public int Intensity { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; }
}
