namespace WhoIAm.Domain.Entities;

public class UserFollower : BaseEntity
{
    public Guid FollowerId { get; set; }
    public Guid FollowingId { get; set; }
    public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
    
    public User? Follower { get; set; }
    public User? Following { get; set; }
}

public class VirtualIdentityFollower : BaseEntity
{
    public Guid FollowerId { get; set; }
    public Guid FollowingId { get; set; }
    public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
    
    public VirtualIdentity? Follower { get; set; }
    public VirtualIdentity? Following { get; set; }
}
