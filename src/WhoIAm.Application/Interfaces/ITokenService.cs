namespace WhoIAm.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, string username);
    string GenerateRefreshToken();
    (Guid? userId, string? error) ValidateToken(string token);
}
