namespace WhoIAm.Domain.Entities;

public class ContentReport : BaseEntity
{
    public Guid ReporterId { get; set; }
    public Guid? ReportedPostId { get; set; }
    public Guid? ReportedUserId { get; set; }
    public Guid? ReportedCommentId { get; set; }
    public ReportReason Reason { get; set; }
    public string? Description { get; set; }
    public ReportStatus Status { get; set; } = ReportStatus.Pending;
    public DateTime? ReviewedAt { get; set; }
    public Guid? ReviewedByUserId { get; set; }
    public string? ReviewNotes { get; set; }
    
    public User? Reporter { get; set; }
    public Post? ReportedPost { get; set; }
    public User? ReportedUser { get; set; }
    public PostComment? ReportedComment { get; set; }
    public User? ReviewedByUser { get; set; }
}

public enum ReportReason
{
    Harassment,
    Threat,
    Hate,
    Spam,
    Scam,
    SexualExploitation,
    NonConsentualIntimate,
    Doxxing,
    Violence,
    DangerousContent,
    Impersonation,
    Other
}

public enum ReportStatus
{
    Pending,
    Reviewing,
    Approved,
    Rejected,
    Escalated
}
