using SmartBA.Application.DTOs.Authentication;

namespace SmartBA.Application.Interfaces.Authentication;
public interface IUserMeService
{

    Task<UserMeProfileDto?> GetProfileAsync(Guid userId);
    Task<UserMeProfileDto?> GetByEmailAsync(string email);

    Task<UserMeProfileDto?> GetByIdAsync(Guid id);

    Task<UserMeProfileDto?> CreateGoogleUserAsync(
        string email,
        string fullName);

    Task<bool> IsLockedAsync(Guid userId);

    Task<int> IncreaseFailedAttemptAsync(Guid userId);

    Task<int> ResetFailedAttemptAsync(Guid userId);
}