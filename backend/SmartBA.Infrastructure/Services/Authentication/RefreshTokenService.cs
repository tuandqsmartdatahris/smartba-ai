using SmartBA.Application.DTOs.Authentication;
using SmartBA.Application.Interfaces.Authentication;
using SmartBA.Infrastructure.Persistence;
namespace SmartBA.Infrastructure.Services.Authentication;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly ApplicationDbContext _db;

    public RefreshTokenService(
        ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TokenResponseDto> RefreshAsync(
        RefreshTokenRequestDto request,
        string? ipAddress,
        string? browser)
    {
        throw new NotImplementedException();
    }
}
