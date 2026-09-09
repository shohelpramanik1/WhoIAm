namespace WhoIAm.Application.DTOs.Community;

public class CreateCommunityRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string Type { get; set; } = "Public"; // Public, Private, Anonymous, Temporary, Paid
    public string? Rules { get; set; }
    public DateTime? ExpiresAt { get; set; }
}

public class CommunityResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string? Banner { get; set; }
    public string Type { get; set; } = string.Empty;
    public int MemberCount { get; set; }
    public int PostCount { get; set; }
    public bool IsJoined { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CommunityDetailResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string? Banner { get; set; }
    public string? Rules { get; set; }
    public string Type { get; set; } = string.Empty;
    public int MemberCount { get; set; }
    public int PostCount { get; set; }
    public bool IsJoined { get; set; }
    public bool IsModerator { get; set; }
    public DateTime CreatedAt { get; set; }
}
