namespace WhoIAm.Domain.Entities;

public class AuditLog : BaseEntity
{
    public Guid? UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Entity { get; set; } = string.Empty;
    public Guid? EntityId { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public AuditLogLevel Level { get; set; } = AuditLogLevel.Info;
    
    public User? User { get; set; }
}

public enum AuditLogLevel
{
    Info,
    Warning,
    Error,
    Critical
}
