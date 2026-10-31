using SmartBA.Application.DTOs.Auth;
using SmartBA.Application.Interfaces.Auth;

using SmartBA.Domain.Entities;

namespace SmartBA.Application.Services.Auth;
public class UserService : IUserService
{
    public Task<UserProfileDto?> GetProfileAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
    public Task<UserProfileDto?> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<UserProfileDto?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserProfileDto?> CreateGoogleUserAsync(
        string email,
        string fullName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsLockedAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<int> IncreaseFailedAttemptAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<int> ResetFailedAttemptAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}