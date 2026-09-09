using WhoIAm.Application.DTOs.Common;
using WhoIAm.Application.DTOs.Journal;

namespace WhoIAm.Application.Interfaces;

public interface IJournalService
{
    Task<JournalEntryResponse> CreateEntryAsync(Guid userId, Guid identityId, CreateJournalEntryRequest request);
    Task<JournalEntryResponse?> GetEntryByIdAsync(Guid entryId, Guid userId);
    Task<PagedResult<JournalEntryResponse>> GetEntriesAsync(Guid userId, Guid identityId, int pageNumber = 1, int pageSize = 20);
    Task<bool> DeleteEntryAsync(Guid entryId, Guid userId);
}
