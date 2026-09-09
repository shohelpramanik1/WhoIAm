namespace WhoIAm.Application.DTOs.VirtualIdentity;

public class CreateVirtualIdentityRequest
{
    public string DisplayName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? Avatar { get; set; }
    public string? Bio { get; set; }
    public int QuietudeLevel { get; set; } = 5;
    public int CalmLevel { get; set; } = 5;
    public int SeriousnessLevel { get; set; } = 5;
    public int IntroversionLevel { get; set; } = 5;
    public int TraditionalismLevel { get; set; } = 5;
    public List<string> Interests { get; set; } = new();
}

public class UpdateVirtualIdentityRequest
{
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
    public string? Avatar { get; set; }
    public int? QuietudeLevel { get; set; }
    public int? CalmLevel { get; set; }
    public int? SeriousnessLevel { get; set; }
    public int? IntroversionLevel { get; set; }
    public int? TraditionalismLevel { get; set; }
    public List<string>? Interests { get; set; }
}

public class VirtualIdentityResponse
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? Avatar { get; set; }
    public string? Bio { get; set; }
    public int FollowerCount { get; set; }
    public int FollowingCount { get; set; }
    public int PostCount { get; set; }
    public int QuietudeLevel { get; set; }
    public int CalmLevel { get; set; }
    public int SeriousnessLevel { get; set; }
    public int IntroversionLevel { get; set; }
    public int TraditionalismLevel { get; set; }
    public string? CurrentMood { get; set; }
    public List<string> Interests { get; set; } = new();
    public bool IsDefault { get; set; }
    public DateTime CreatedAt { get; set; }
}
