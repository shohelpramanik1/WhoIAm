namespace WhoIAm.Domain.Entities;

public class PostComment : BaseEntity
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public Guid? ParentCommentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsAnonymous { get; set; }
    public int ReactionCount { get; set; }
    public int ReplyCount { get; set; }
    
    public Post? Post { get; set; }
    public User? User { get; set; }
    public PostComment? ParentComment { get; set; }
    public ICollection<PostComment> Replies { get; set; } = new List<PostComment>();
    public ICollection<PostCommentReaction> Reactions { get; set; } = new List<PostCommentReaction>();
}

public class PostCommentReaction : BaseEntity
{
    public Guid CommentId { get; set; }
    public Guid UserId { get; set; }
    public ReactionType Type { get; set; }
    
    public PostComment? Comment { get; set; }
    public User? User { get; set; }
}
