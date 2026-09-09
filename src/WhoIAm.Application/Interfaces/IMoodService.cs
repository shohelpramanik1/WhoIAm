using WhoIAm.Application.DTOs.Common;
using WhoIAm.Application.DTOs.Mood;

namespace WhoIAm.Application.Interfaces;

public interface IMoodService
{
    Task<MoodResponse> CreateMoodAsync(Guid userId, Guid identityId, CreateMoodRequest request);
    Task<List<MoodResponse>> GetMoodHistoryAsync(Guid userId, Guid identityId, int days = 7);
}
