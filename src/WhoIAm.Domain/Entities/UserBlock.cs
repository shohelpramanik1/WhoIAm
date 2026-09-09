namespace WhoIAm.Domain.Entities;

public class UserBlock : BaseEntity
{
    public Guid BlockerId { get; set; }
    public Guid BlockedId { get; set; }
    public string? Reason { get; set; }
    public DateTime BlockedAt { get; set; } = DateTime.UtcNow;
    
    public User? Blocker { get; set; }
    public User? Blocked { get; set; }
}

public class UserMute : BaseEntity
{
    public Guid MuterId { get; set; }
    public Guid MutedId { get; set; }
    public DateTime MutedAt { get; set; } = DateTime.UtcNow;
    
    public User? Muter { get; set; }
    public User? Muted { get; set; }
}
