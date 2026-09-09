namespace WhoIAm.Domain.Entities;

public class UserSession : BaseEntity
{
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public bool IsRevoked { get; set; }
    
    public User? User { get; set; }
}
