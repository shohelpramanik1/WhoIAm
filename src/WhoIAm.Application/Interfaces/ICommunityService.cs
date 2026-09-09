using WhoIAm.Application.DTOs.Community;
using WhoIAm.Application.DTOs.Common;

namespace WhoIAm.Application.Interfaces;

public interface ICommunityService
{
    Task<CommunityResponse> CreateCommunityAsync(Guid userId, CreateCommunityRequest request);
    Task<CommunityDetailResponse?> GetCommunityByIdAsync(Guid communityId, Guid? userId = null);
    Task<CommunityDetailResponse?> GetCommunityBySlugAsync(string slug, Guid? userId = null);
    Task<PagedResult<CommunityResponse>> GetCommunitiesAsync(int pageNumber = 1, int pageSize = 20);
    Task<bool> JoinCommunityAsync(Guid communityId, Guid userId);
    Task<bool> LeaveCommunityAsync(Guid communityId, Guid userId);
}
