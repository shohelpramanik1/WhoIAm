using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhoIAm.Application.DTOs.Common;
using WhoIAm.Application.DTOs.Community;
using WhoIAm.Application.Interfaces;

namespace WhoIAm.Api.Controllers;

[ApiController]
[Route("api/[controller}")]
public class CommunitiesController : ControllerBase
{
    private readonly ICommunityService _communityService;
    private readonly ILogger<CommunitiesController> _logger;

    public CommunitiesController(ICommunityService communityService, ILogger<CommunitiesController> logger)
    {
        _communityService = communityService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<CommunityResponse>>>> GetCommunities(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _communityService.GetCommunitiesAsync(pageNumber, pageSize);
        
        return Ok(new ApiResponse<PagedResult<CommunityResponse>>
        {
            Success = true,
            Data = result
        });
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<ApiResponse<CommunityDetailResponse>>> GetCommunity(string slug)
    {
        var result = await _communityService.GetCommunityBySlugAsync(slug);
        
        if (result == null)
        {
            return NotFound(new ApiResponse { Success = false, Message = "Community not found" });
        }

        return Ok(new ApiResponse<CommunityDetailResponse>
        {
            Success = true,
            Data = result
        });
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<CommunityResponse>>> CreateCommunity([FromBody] CreateCommunityRequest request)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        _logger.LogInformation("Creating community for user: {UserId}", userId);
        
        var result = await _communityService.CreateCommunityAsync(userId, request);

        return Ok(new ApiResponse<CommunityResponse>
        {
            Success = true,
            Data = result
        });
    }

    [HttpPost("{communityId:guid}/join")]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> JoinCommunity(Guid communityId)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var success = await _communityService.JoinCommunityAsync(communityId, userId);
        
        return Ok(new ApiResponse
        {
            Success = success,
            Message = success ? "Joined community" : "Failed to join community"
        });
    }
}
