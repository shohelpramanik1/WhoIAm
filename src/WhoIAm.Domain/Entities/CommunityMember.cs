namespace WhoIAm.Domain.Entities;

public class CommunityMember : BaseEntity
{
    public Guid CommunityId { get; set; }
    public Guid UserId { get; set; }
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    
    public Community? Community { get; set; }
    public User? User { get; set; }
}

public class CommunityModerator : BaseEntity
{
    public Guid CommunityId { get; set; }
    public Guid UserId { get; set; }
    public ModeratorLevel Level { get; set; } = ModeratorLevel.Moderator;
    
    public Community? Community { get; set; }
    public User? User { get; set; }
}

public enum ModeratorLevel
{
    Moderator,
    Senior,
    Owner
}
