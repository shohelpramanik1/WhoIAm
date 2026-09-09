using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhoIAm.Application.DTOs.Common;
using WhoIAm.Application.DTOs.Post;
using WhoIAm.Application.Interfaces;

namespace WhoIAm.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly ILogger<PostsController> _logger;

    public PostsController(IPostService postService, ILogger<PostsController> logger)
    {
        _postService = postService;
        _logger = logger;
    }

    [HttpGet("feed")]
    public async Task<ActionResult<ApiResponse<PagedResult<PostResponse>>>> GetFeed(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var result = await _postService.GetFeedAsync(userId, pageNumber, pageSize);
        
        return Ok(new ApiResponse<PagedResult<PostResponse>>
        {
            Success = true,
            Data = result
        });
    }

    [HttpPost("create")]
    public async Task<ActionResult<ApiResponse<PostResponse>>> CreatePost([FromBody] CreatePostRequest request)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        _logger.LogInformation("Creating post for user: {UserId}", userId);
        
        var result = await _postService.CreatePostAsync(userId, null, request);

        return Ok(new ApiResponse<PostResponse>
        {
            Success = true,
            Data = result
        });
    }

    [HttpGet("{postId:guid}")]
    public async Task<ActionResult<ApiResponse<PostResponse>>> GetPost(Guid postId)
    {
        var result = await _postService.GetPostByIdAsync(postId);
        
        if (result == null)
        {
            return NotFound(new ApiResponse { Success = false, Message = "Post not found" });
        }

        return Ok(new ApiResponse<PostResponse>
        {
            Success = true,
            Data = result
        });
    }

    [HttpPost("{postId:guid}/like")]
    public async Task<ActionResult<ApiResponse>> LikePost(Guid postId)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var success = await _postService.LikePostAsync(postId, userId);
        
        return Ok(new ApiResponse
        {
            Success = success,
            Message = success ? "Post liked" : "Failed to like post"
        });
    }
}
