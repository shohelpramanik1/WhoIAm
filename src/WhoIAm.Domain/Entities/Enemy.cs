namespace WhoIAm.Domain.Entities;

public class Enemy : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid VirtualIdentityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Represents { get; set; } = string.Empty;
    public string? Personality { get; set; }
    public string? Avatar { get; set; }
    public EnemyCategory Category { get; set; } = EnemyCategory.Other;
    public bool IsTemplate { get; set; }
    public int SessionCount { get; set; }
    
    public User? User { get; set; }
    public VirtualIdentity? VirtualIdentity { get; set; }
    public ICollection<EnemySession> Sessions { get; set; } = new List<EnemySession>();
}

public class EnemySession : BaseEntity
{
    public Guid EnemyId { get; set; }
    public string? SessionNotes { get; set; }
    public string? Reflection { get; set; }
    public int InteractionCount { get; set; }
    public DateTime? CompletedAt { get; set; }
    
    public Enemy? Enemy { get; set; }
    public ICollection<EnemyMessage> Messages { get; set; } = new List<EnemyMessage>();
}

public class EnemyMessage : BaseEntity
{
    public Guid EnemySessionId { get; set; }
    public string UserMessage { get; set; } = string.Empty;
    public string? EnemyResponse { get; set; }
    public MessageType Type { get; set; }
    
    public EnemySession? EnemySession { get; set; }
}

public enum EnemyCategory
{
    Fear,
    Stress,
    Failure,
    Pressure,
    Anger,
    Regret,
    Loneliness,
    SelfDoubt,
    Procrastination,
    Expectations,
    Other
}

public enum MessageType
{
    UserMessage,
    EnemyResponse,
    SystemMessage
}
