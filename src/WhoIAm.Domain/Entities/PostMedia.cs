namespace WhoIAm.Domain.Entities;

public class PostMedia : BaseEntity
{
    public Guid PostId { get; set; }
    public string Url { get; set; } = string.Empty;
    public string MediaType { get; set; } = string.Empty; // image, video, audio
    public string? ThumbnailUrl { get; set; }
    public long SizeBytes { get; set; }
    public int DisplayOrder { get; set; }
    
    public Post? Post { get; set; }
}
