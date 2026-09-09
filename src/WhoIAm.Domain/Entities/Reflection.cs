namespace WhoIAm.Domain.Entities;

public class ReflectionEntry : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid VirtualIdentityId { get; set; }
    public Guid? EnemySessionId { get; set; }
    public string? MirrorQuestion { get; set; }
    public string? MirrorResponse { get; set; }
    public string? Discovered { get; set; }
    public ReflectionCategory? Category { get; set; }
    
    public User? User { get; set; }
    public VirtualIdentity? VirtualIdentity { get; set; }
}

public enum ReflectionCategory
{
    NeedRest,
    NeedConnection,
    NeedConfidence,
    NeedBoundaries,
    NeedForgiveness,
    NeedMotivation,
    NeedToChange,
    StillThinking
}
