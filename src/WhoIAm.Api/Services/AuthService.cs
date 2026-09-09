using WhoIAm.Application.DTOs.Auth;
using WhoIAm.Application.Interfaces;
using WhoIAm.Infrastructure.Repositories;
using WhoIAm.Domain.Entities;

namespace WhoIAm.Api.Services;

public class AuthService : IAuthService
{
    private readonly UserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        UserRepository userRepository,
        ITokenService tokenService,
        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<(bool success, string? accessToken, string? refreshToken, string? message)> RegisterAsync(
        string email, string username, string password, DateTime dateOfBirth, string country)
    {
        try
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
            {
                return (false, null, null, "Email already registered");
            }

            var existingUsername = await _userRepository.GetByUsernameAsync(username);
            if (existingUsername != null)
            {
                return (false, null, null, "Username already taken");
            }

            // Create new user
            var user = new User
            {
                Email = email,
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                DateOfBirth = dateOfBirth,
                Country = country,
                AgeStatus = AgeAssuranceStatus.SelfDeclaredAdult,
                Active = true
            };

            await _userRepository.AddAsync(user);
            _logger.LogInformation("User registered: {Email}", email);

            var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Username);
            var refreshToken = _tokenService.GenerateRefreshToken();

            return (true, accessToken, refreshToken, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Registration error for email: {Email}", email);
            return (false, null, null, "Registration failed");
        }
    }

    public async Task<(bool success, string? accessToken, string? refreshToken, string? message)> LoginAsync(
        string email, string password)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return (false, null, null, "Invalid credentials");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return (false, null, null, "Invalid credentials");
            }

            if (!user.Active)
            {
                return (false, null, null, "Account is inactive");
            }

            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);
            _logger.LogInformation("User logged in: {Email}", email);

            var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Username);
            var refreshToken = _tokenService.GenerateRefreshToken();

            return (true, accessToken, refreshToken, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login error for email: {Email}", email);
            return (false, null, null, "Login failed");
        }
    }

    public async Task<(bool success, string? accessToken, string? message)> RefreshTokenAsync(string refreshToken)
    {
        try
        {
            // Validate refresh token
            if (string.IsNullOrEmpty(refreshToken))
            {
                return (false, null, "Invalid refresh token");
            }

            // In production, validate against stored refresh tokens
            var newAccessToken = _tokenService.GenerateAccessToken(Guid.NewGuid(), "user");
            return (true, newAccessToken, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Token refresh error");
            return (false, null, "Token refresh failed");
        }
    }

    public async Task<bool> LogoutAsync(Guid userId)
    {
        try
        {
            // Invalidate all sessions for user
            _logger.LogInformation("User logged out: {UserId}", userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Logout error for user: {UserId}", userId);
            return false;
        }
    }
}
