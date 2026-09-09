namespace WhoIAm.Application.Interfaces;

public interface IVirtualIdentityService
{
    Task<(bool success, string? message)> CreateIdentityAsync(Guid userId, string displayName, string username);
    Task<(bool success, string? message)> SwitchIdentityAsync(Guid userId, Guid identityId);
    Task<(bool success, string? message)> DeleteIdentityAsync(Guid userId, Guid identityId);
}
