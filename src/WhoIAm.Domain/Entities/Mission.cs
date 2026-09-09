namespace WhoIAm.Domain.Entities;

public class RealLifeMission : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid VirtualIdentityId { get; set; }
    public Guid? ReflectionEntryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int EstimatedMinutes { get; set; }
    public MissionDifficulty Difficulty { get; set; } = MissionDifficulty.Easy;
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? DueDate { get; set; }
    
    public User? User { get; set; }
    public VirtualIdentity? VirtualIdentity { get; set; }
}

public enum MissionDifficulty
{
    Easy,
    Medium,
    Hard
}
