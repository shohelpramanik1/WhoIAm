namespace WhoIAm.Application.Interfaces;

public interface IAuthService
{
    Task<(bool success, string? accessToken, string? refreshToken, string? message)> RegisterAsync(
        string email, string username, string password, DateTime dateOfBirth, string country);
    
    Task<(bool success, string? accessToken, string? refreshToken, string? message)> LoginAsync(
        string email, string password);
    
    Task<(bool success, string? accessToken, string? message)> RefreshTokenAsync(string refreshToken);
    
    Task<bool> LogoutAsync(Guid userId);
}
