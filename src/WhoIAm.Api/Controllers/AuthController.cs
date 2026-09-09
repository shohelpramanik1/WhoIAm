using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhoIAm.Application.DTOs.Auth;
using WhoIAm.Application.DTOs.Common;
using WhoIAm.Application.Interfaces;
using WhoIAm.Infrastructure.Repositories;

namespace WhoIAm.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ITokenService tokenService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _tokenService = tokenService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> Register([FromBody] RegisterRequest request)
    {
        _logger.LogInformation("Registration attempt for email: {Email}", request.Email);
        
        var (success, accessToken, refreshToken, message) = await _authService.RegisterAsync(
            request.Email, request.Username, request.Password, request.DateOfBirth, request.Country);

        if (!success)
        {
            return BadRequest(new ApiResponse { Success = false, Message = message });
        }

        var response = new ApiResponse<AuthResponse>
        {
            Success = true,
            Data = new AuthResponse
            {
                AccessToken = accessToken!,
                RefreshToken = refreshToken!,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15)
            }
        };

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> Login([FromBody] LoginRequest request)
    {
        _logger.LogInformation("Login attempt for email: {Email}", request.Email);
        
        var (success, accessToken, refreshToken, message) = await _authService.LoginAsync(
            request.Email, request.Password);

        if (!success)
        {
            return Unauthorized(new ApiResponse { Success = false, Message = message });
        }

        var response = new ApiResponse<AuthResponse>
        {
            Success = true,
            Data = new AuthResponse
            {
                AccessToken = accessToken!,
                RefreshToken = refreshToken!,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15)
            }
        };

        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var (success, accessToken, message) = await _authService.RefreshTokenAsync(request.RefreshToken);

        if (!success)
        {
            return Unauthorized(new ApiResponse { Success = false, Message = message });
        }

        var response = new ApiResponse<AuthResponse>
        {
            Success = true,
            Data = new AuthResponse
            {
                AccessToken = accessToken!,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15)
            }
        };

        return Ok(response);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> Logout()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var success = await _authService.LogoutAsync(userId);
        
        return Ok(new ApiResponse
        {
            Success = success,
            Message = success ? "Logged out successfully" : "Failed to logout"
        });
    }
}
