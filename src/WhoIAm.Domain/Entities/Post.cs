namespace WhoIAm.Domain.Entities;

public class Post : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid? VirtualIdentityId { get; set; }
    public Guid? CommunityId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsAnonymous { get; set; }
    public PostVisibility Visibility { get; set; } = PostVisibility.Public;
    public PostType Type { get; set; } = PostType.Text;
    public int ReactionCount { get; set; }
    public int CommentCount { get; set; }
    public int ShareCount { get; set; }
    public int SaveCount { get; set; }
    public string? CurrentMood { get; set; }
    
    public User? User { get; set; }
    public VirtualIdentity? VirtualIdentity { get; set; }
    public Community? Community { get; set; }
    public ICollection<PostMedia> Media { get; set; } = new List<PostMedia>();
    public ICollection<PostReaction> Reactions { get; set; } = new List<PostReaction>();
    public ICollection<PostComment> Comments { get; set; } = new List<PostComment>();
    public ICollection<PostReport> Reports { get; set; } = new List<PostReport>();
}

public enum PostVisibility
{
    Public,
    Followers,
    Community,
    Private
}

public enum PostType
{
    Text,
    Image,
    Video,
    Audio,
    Poll,
    Confession,
    Story
}
