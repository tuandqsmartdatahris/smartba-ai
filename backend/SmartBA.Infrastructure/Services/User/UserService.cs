using Microsoft.EntityFrameworkCore;
using SmartBA.Application.DTOs.Auth;
using SmartBA.Infrastructure.Persistence;
using SmartBA.Domain.Entities;
using SmartBA.Application.Interfaces.Auth;

namespace SmartBA.Infrastructure.Services.User;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _db;

    public UserService(
        ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<UserProfileDto?> GetProfileAsync(
        Guid userId)
    {
        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            return null;
        }

        return new UserProfileDto
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role,
            AvatarUrl = user.AvatarUrl,
            IsActive = user.IsActive,
            CreatedDate = user.CreatedDate
        };
    }

    public async Task<UserProfileDto?> GetByEmailAsync(string email)
    {
        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
        {
            return null;
        }
        
        return new UserProfileDto
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role,
            AvatarUrl = user.AvatarUrl,
            IsActive = user.IsActive,
            CreatedDate = user.CreatedDate
        };
    }
    public async Task<UserProfileDto?> GetByIdAsync(Guid userId)
    {
        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            return null;
        }
        
        return new UserProfileDto
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role,
            AvatarUrl = user.AvatarUrl,
            IsActive = user.IsActive,
            CreatedDate = user.CreatedDate
        };
    }

    public async Task<UserProfileDto?> CreateGoogleUserAsync(string email,
        string fullName)
    {
        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
        {
            return null;
        }
        
        return new UserProfileDto
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role,
            AvatarUrl = user.AvatarUrl,
            IsActive = user.IsActive,
            CreatedDate = user.CreatedDate
        };
    }

    public async Task<bool> IsLockedAsync (Guid userId)
    {
        var _user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (_user == null)
        {
            return false;
        }
        
        return _user.IsLocked;
    }

        public async Task<int> IncreaseFailedAttemptAsync (Guid userId)
        {
           return 1;
        }

        public async Task<int> ResetFailedAttemptAsync (Guid userId)
        {
            return 1;
        }

}
  