namespace SmartBA.Application.Interfaces.Authentication;
using SmartBA.Application.DTOs.Authentication;

public interface IRefreshTokenService
{
        // Trả về AccessToken và RefreshToken
        Task<TokenResponseDto> RefreshAsync(
                RefreshTokenRequestDto request, // Chứa token gởi lên
                string? ipAddress,
                string? browser);
}