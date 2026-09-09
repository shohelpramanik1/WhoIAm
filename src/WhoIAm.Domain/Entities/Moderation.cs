namespace WhoIAm.Domain.Entities;

public class ModerationAction : BaseEntity
{
    public Guid ContentReportId { get; set; }
    public Guid ActionTakenByUserId { get; set; }
    public ModerationDecision Decision { get; set; }
    public string? Reason { get; set; }
    public DateTime? AppealDeadline { get; set; }
    public bool IsAppealed { get; set; }
    
    public ContentReport? ContentReport { get; set; }
    public User? ActionTakenByUser { get; set; }
    public ICollection<ModerationAppeal> Appeals { get; set; } = new List<ModerationAppeal>();
}

public class ModerationAppeal : BaseEntity
{
    public Guid ModerationActionId { get; set; }
    public Guid AppealedByUserId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public AppealStatus Status { get; set; } = AppealStatus.Pending;
    public DateTime? ReviewedAt { get; set; }
    public Guid? ReviewedByUserId { get; set; }
    public string? ReviewNotes { get; set; }
    
    public ModerationAction? ModerationAction { get; set; }
    public User? AppealedByUser { get; set; }
    public User? ReviewedByUser { get; set; }
}

public enum ModerationDecision
{
    NoAction,
    Warning,
    ContentRemoved,
    TemporarySuspension,
    PermanentBan,
    Escalated
}

public enum AppealStatus
{
    Pending,
    Approved,
    Rejected
}
