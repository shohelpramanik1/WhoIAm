namespace WhoIAm.Application.DTOs.Post;

public class CreatePostRequest
{
    public string Content { get; set; } = string.Empty;
    public bool IsAnonymous { get; set; }
    public string Visibility { get; set; } = "Public"; // Public, Followers, Community, Private
    public string Type { get; set; } = "Text"; // Text, Image, Video, Audio, Poll, Confession, Story
    public Guid? CommunityId { get; set; }
    public string? CurrentMood { get; set; }
    public List<IFormFile>? MediaFiles { get; set; }
}

public class PostResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsAnonymous { get; set; }
    public string Visibility { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? CurrentMood { get; set; }
    public int ReactionCount { get; set; }
    public int CommentCount { get; set; }
    public int ShareCount { get; set; }
    public int SaveCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public VirtualIdentityResponse? VirtualIdentity { get; set; }
    public List<PostMediaResponse> Media { get; set; } = new();
}

public class PostMediaResponse
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string MediaType { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public int DisplayOrder { get; set; }
}
