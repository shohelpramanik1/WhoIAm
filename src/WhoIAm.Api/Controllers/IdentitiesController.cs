using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhoIAm.Application.DTOs.Common;
using WhoIAm.Application.DTOs.VirtualIdentity;
using WhoIAm.Application.Interfaces;

namespace WhoIAm.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IdentitiesController : ControllerBase
{
    private readonly IVirtualIdentityService _identityService;
    private readonly ILogger<IdentitiesController> _logger;

    public IdentitiesController(IVirtualIdentityService identityService, ILogger<IdentitiesController> logger)
    {
        _identityService = identityService;
        _logger = logger;
    }

    [HttpPost("create")]
    public async Task<ActionResult<ApiResponse>> CreateIdentity([FromBody] CreateVirtualIdentityRequest request)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        _logger.LogInformation("Creating identity for user: {UserId}", userId);
        
        var (success, message) = await _identityService.CreateIdentityAsync(
            userId, request.DisplayName, request.Username);

        if (!success)
        {
            return BadRequest(new ApiResponse { Success = false, Message = message });
        }

        return Ok(new ApiResponse { Success = true, Message = "Identity created successfully" });
    }

    [HttpPost("switch/{identityId:guid}")]
    public async Task<ActionResult<ApiResponse>> SwitchIdentity(Guid identityId)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        _logger.LogInformation("Switching identity for user: {UserId} to: {IdentityId}", userId, identityId);
        
        var (success, message) = await _identityService.SwitchIdentityAsync(userId, identityId);

        if (!success)
        {
            return BadRequest(new ApiResponse { Success = false, Message = message });
        }

        return Ok(new ApiResponse { Success = true, Message = "Identity switched successfully" });
    }
}
