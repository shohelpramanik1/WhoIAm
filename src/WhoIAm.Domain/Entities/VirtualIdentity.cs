namespace WhoIAm.Domain.Entities;

public class VirtualIdentity : BaseEntity
{
    public Guid UserId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? Avatar { get; set; }
    public string? Bio { get; set; }
    public IdentityVisibility Visibility { get; set; } = IdentityVisibility.Public;
    public int FollowerCount { get; set; }
    public int FollowingCount { get; set; }
    public int PostCount { get; set; }
    
    // Personality settings
    public int QuietudeLevel { get; set; } = 5; // 1-10: Quiet to Social
    public int CalmLevel { get; set; } = 5;     // 1-10: Calm to Intense
    public int SeriousnessLevel { get; set; } = 5; // 1-10: Serious to Funny
    public int IntroversionLevel { get; set; } = 5; // 1-10: Introvert to Extrovert
    public int TraditionalismLevel { get; set; } = 5; // 1-10: Traditional to Rebellious
    
    public string? CurrentMood { get; set; }
    public bool IsDefault { get; set; }
    
    public User? User { get; set; }
    public ICollection<IdentityInterest> Interests { get; set; } = new List<IdentityInterest>();
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<VirtualIdentityFollower> Followers { get; set; } = new List<VirtualIdentityFollower>();
    public ICollection<VirtualIdentityFollower> Following { get; set; } = new List<VirtualIdentityFollower>();
    public ICollection<MoodEntry> MoodEntries { get; set; } = new List<MoodEntry>();
    public ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
}

public enum IdentityVisibility
{
    Public,
    FollowersOnly,
    Private
}
