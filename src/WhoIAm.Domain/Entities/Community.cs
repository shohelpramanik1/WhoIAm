namespace WhoIAm.Domain.Entities;

public class Community : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string? Banner { get; set; }
    public string? Rules { get; set; }
    public CommunityType Type { get; set; } = CommunityType.Public;
    public CommunityCategory Category { get; set; } = CommunityCategory.General;
    public Guid CreatedByUserId { get; set; }
    public int MemberCount { get; set; }
    public int PostCount { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsTemporary { get; set; }
    
    public User? CreatedByUser { get; set; }
    public ICollection<CommunityMember> Members { get; set; } = new List<CommunityMember>();
    public ICollection<CommunityModerator> Moderators { get; set; } = new List<CommunityModerator>();
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}

public enum CommunityType
{
    Public,
    Private,
    Anonymous,
    Temporary,
    Paid
}

public enum CommunityCategory
{
    General,
    Mental,
    Relationships,
    Career,
    Creativity,
    Gaming,
    Entertainment,
    Lifestyle,
    Support,
    Discussion
}
