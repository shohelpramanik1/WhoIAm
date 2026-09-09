using WhoIAm.Application.DTOs.Common;
using WhoIAm.Application.DTOs.Enemy;

namespace WhoIAm.Application.Interfaces;

public interface IEnemyService
{
    Task<EnemyResponse> CreateEnemyAsync(Guid userId, Guid identityId, CreateEnemyRequest request);
    Task<EnemyResponse?> GetEnemyByIdAsync(Guid enemyId, Guid userId);
    Task<PagedResult<EnemyResponse>> GetUserEnemiesAsync(Guid userId, Guid identityId, int pageNumber = 1, int pageSize = 20);
    Task<EnemySessionResponse> CreateEnemySessionAsync(Guid enemyId, Guid userId);
    Task<bool> DeleteEnemyAsync(Guid enemyId, Guid userId);
}
