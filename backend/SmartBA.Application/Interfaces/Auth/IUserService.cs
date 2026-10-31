using SmartBA.Domain.Entities;
namespace SmartBA.Application.Interfaces.Auth;
using SmartBA.Application.DTOs.Auth;

public interface IUserService
{

    Task<UserProfileDto?> GetProfileAsync(Guid userId);
    Task<UserProfileDto?> GetByEmailAsync(string email);

    Task<UserProfileDto?> GetByIdAsync(Guid id);

    Task<UserProfileDto?> CreateGoogleUserAsync(
        string email,
        string fullName);

    Task<bool> IsLockedAsync(Guid userId);

    Task<int> IncreaseFailedAttemptAsync(Guid userId);

    Task<int> ResetFailedAttemptAsync(Guid userId);
}