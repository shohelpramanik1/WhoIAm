namespace WhoIAm.Domain.Entities;

public class PostReaction : BaseEntity
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public ReactionType Type { get; set; }
    
    public Post? Post { get; set; }
    public User? User { get; set; }
}

public enum ReactionType
{
    Like,
    Love,
    Support,
    Interesting,
    Funny,
    Relatable
}
