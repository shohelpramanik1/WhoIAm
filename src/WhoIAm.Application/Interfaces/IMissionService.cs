using WhoIAm.Application.DTOs.Common;
using WhoIAm.Application.DTOs.Mission;

namespace WhoIAm.Application.Interfaces;

public interface IMissionService
{
    Task<MissionResponse> CreateMissionAsync(Guid userId, Guid identityId, CreateMissionRequest request);
    Task<MissionResponse?> GetMissionByIdAsync(Guid missionId, Guid userId);
    Task<PagedResult<MissionResponse>> GetUserMissionsAsync(Guid userId, Guid identityId, int pageNumber = 1, int pageSize = 20);
    Task<bool> CompleteMissionAsync(Guid missionId, Guid userId);
    Task<bool> DeleteMissionAsync(Guid missionId, Guid userId);
}
