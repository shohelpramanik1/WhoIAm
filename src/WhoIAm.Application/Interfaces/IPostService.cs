using WhoIAm.Application.DTOs.Common;
using WhoIAm.Application.DTOs.Post;

namespace WhoIAm.Application.Interfaces;

public interface IPostService
{
    Task<PostResponse> CreatePostAsync(Guid userId, Guid? identityId, CreatePostRequest request);
    Task<PostResponse?> GetPostByIdAsync(Guid postId, Guid? userId = null);
    Task<PagedResult<PostResponse>> GetFeedAsync(Guid userId, int pageNumber = 1, int pageSize = 20);
    Task<PagedResult<PostResponse>> GetUserPostsAsync(Guid userId, int pageNumber = 1, int pageSize = 20);
    Task<bool> DeletePostAsync(Guid postId, Guid userId);
    Task<bool> LikePostAsync(Guid postId, Guid userId);
    Task<bool> UnlikePostAsync(Guid postId, Guid userId);
}
