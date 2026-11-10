using Microsoft.EntityFrameworkCore;
using SmartBA.Application.DTOs.Authentication;
using SmartBA.Infrastructure.Persistence;
using SmartBA.Domain.Entities.Authentication;
using SmartBA.Application.Interfaces.Authentication;

namespace SmartBA.Infrastructure.Services.User;

public class UserMeService : IUserMeService
{
    private readonly ApplicationDbContext _db;

    public UserMeService(
        ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<UserMeProfileDto?> GetProfileAsync(
        Guid userId)
    {
        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            return null;
        }

        return new UserMeProfileDto
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

    public async Task<UserMeProfileDto?> GetByEmailAsync(string email)
    {
        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
        {
            return null;
        }
        
        return new UserMeProfileDto
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
    public async Task<UserMeProfileDto?> GetByIdAsync(Guid userId)
    {
        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            return null;
        }
        
        return new UserMeProfileDto
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

    public async Task<UserMeProfileDto?> CreateGoogleUserAsync(string email,
        string fullName)
    {
        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
        {
            return null;
        }
        
        return new UserMeProfileDto
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
        
        // return _user.IsLocked;
        return _user.IsActive;
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
  