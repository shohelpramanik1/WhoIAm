namespace WhoIAm.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? Avatar { get; set; }
    public string? Bio { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Country { get; set; } = string.Empty;
    public bool EmailVerified { get; set; }
    public DateTime? EmailVerifiedAt { get; set; }
    public AgeAssuranceStatus AgeStatus { get; set; } = AgeAssuranceStatus.Unknown;
    public bool Active { get; set; } = true;
    public Guid? CurrentIdentityId { get; set; }
    public int FollowerCount { get; set; }
    public int FollowingCount { get; set; }
    public DateTime? LastLoginAt { get; set; }
    
    public ICollection<VirtualIdentity> VirtualIdentities { get; set; } = new List<VirtualIdentity>();
    public ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<UserFollower> Followers { get; set; } = new List<UserFollower>();
    public ICollection<UserFollower> Following { get; set; } = new List<UserFollower>();
}

public enum AgeAssuranceStatus
{
    Unknown,
    SelfDeclaredAdult,
    VerifiedAdult,
    Restricted,
    Rejected
}
