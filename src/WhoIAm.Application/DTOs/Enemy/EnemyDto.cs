namespace WhoIAm.Application.DTOs.Enemy;

public class CreateEnemyRequest
{
    public string Name { get; set; } = string.Empty;
    public string Represents { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Personality { get; set; }
    public string? Avatar { get; set; }
    public string Category { get; set; } = "Other"; // Fear, Stress, Failure, etc.
}

public class EnemyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Represents { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Personality { get; set; }
    public string? Avatar { get; set; }
    public string Category { get; set; } = string.Empty;
    public int SessionCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class EnemySessionResponse
{
    public Guid Id { get; set; }
    public Guid EnemyId { get; set; }
    public string? SessionNotes { get; set; }
    public string? Reflection { get; set; }
    public int InteractionCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
