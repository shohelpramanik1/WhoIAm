namespace WhoIAm.Application.DTOs.User;

public class CreateUserRequest
{
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Country { get; set; } = string.Empty;
}

public class UpdateUserRequest
{
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
    public string? Avatar { get; set; }
}

public class UserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
    public string? Avatar { get; set; }
    public int FollowerCount { get; set; }
    public int FollowingCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class UserProfileResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
    public string? Avatar { get; set; }
    public int FollowerCount { get; set; }
    public int FollowingCount { get; set; }
    public int PostCount { get; set; }
    public bool IsFollowing { get; set; }
    public DateTime CreatedAt { get; set; }
}
