using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SmartBA.Infrastructure.Persistence;
using SmartBA.Domain.Enums;
using SmartBA.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SmartBA.Application.DTOs.Authentication;
using SmartBA.Domain.Entities.Authentication;
using SmartBA.Application.Interfaces.Email;
using SmartBA.Application.Interfaces.Authentication;
using SmartBA.Infrastructure.Services.Authentication;


namespace SmartBA.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    
    private readonly ApplicationDbContext _db;
    private readonly IEmailService _emailService;
    private readonly JwtService _jwtService;
    private readonly IUserMeService _userService;

    private readonly IRefreshTokenService _refreshTokenService;

    public AuthController(
        ApplicationDbContext db,
        IEmailService emailService,
        JwtService jwtService,
        IUserMeService userService,
        IRefreshTokenService refreshTokenService)
    {
        _db = db;
        _emailService = emailService;
        _jwtService = jwtService;
        _userService = userService;
        _refreshTokenService = refreshTokenService;
    }

    [HttpGet("google-login")]
    public IActionResult GoogleLogin()
    {
        var properties =
            new AuthenticationProperties
            {
                RedirectUri =
                    "/api/auth/google-callback"
            };

        return Challenge(
            properties,
            GoogleDefaults.AuthenticationScheme);
    }


    [HttpGet("google-callback")]
    public async Task<IActionResult> Callback()
    {
         var email =
            User.FindFirst(ClaimTypes.Email)?.Value;

        var fullName =
            User.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email not found.");
        }

        var user = await _db.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
        {
            user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                FullName = fullName ?? "",
                Role = "Customer",
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            _db.Users.Add(user);

            await _db.SaveChangesAsync();
        }
        
        var lastOtp = await _db.UserOtps
            .Where(x => x.Email == email)
            .OrderByDescending(x => x.CreatedDate)
            .FirstOrDefaultAsync();

        if(lastOtp != null &&
        lastOtp.CreatedDate >
        DateTime.Now.AddMinutes(-1))
        {
            return BadRequest(
                "Please wait 60 seconds");
        }

        // OTP sẽ tạo ở bước tiếp theo

        // ===== TẠO OTP TẠI ĐÂY =====

        var otp = Random.Shared
            .Next(100000, 999999)
            .ToString();

        var userOtp = new UserOtp
        {
            Id = Guid.NewGuid(),
            Email = email,
            OtpCode = SecurityHelper.HashOtp(otp),
            ExpireDate = DateTime.Now.AddMinutes(5),
            IsUsed = false,
            CreatedDate = DateTime.Now
        };

        _db.UserOtps.Add(userOtp);

        await _db.SaveChangesAsync();

        await _emailService.SendOtpAsync(
            email,
            otp);

        return Redirect(
             "http://localhost:4200/verify-otp");
            }

    [HttpPost("verify-otp")]
    public async Task<IActionResult>
        VerifyOtp(
        VerifyOtpRequestDto request)
    {

        var user = await _db.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (user == null)
        {
            return BadRequest("User not found");
        }

        var _userProfileDto =
            new UserMeProfileDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role
            };

        if (user.LockUntil.HasValue &&
            user.LockUntil > DateTime.Now)
        {
            return BadRequest(
                 "Authentication failed");
        }

        var otp = await _db.UserOtps
            .Where(x =>
                x.Email == request.Email &&
                !x.IsUsed &&
                x.ExpireDate > DateTime.Now)
            .OrderByDescending(x => x.CreatedDate)
            .FirstOrDefaultAsync();            

    
        if (otp == null)
        {
                user.FailedAttempts++;

                if (user.FailedAttempts >= 5)
                {
                    user.LockUntil =
                     DateTime.Now.AddMinutes(15);

                    user.FailedAttempts = 0;
                }
                

                _db.LoginLogs.Add(new LoginLog
                        {
                            Id = Guid.NewGuid(),
                            UserId = user.Id,
                            SessionId = Guid.Empty,
                            LoginTime = DateTime.Now,
                            IpAddress =
                                HttpContext.Connection.RemoteIpAddress?.ToString(),
                            Browser =
                                Request.Headers.UserAgent.ToString(),
                            Action = "OTP_FAILED",
                            IsSuccess = false
                        });

                await _db.SaveChangesAsync();

                return BadRequest("Invalid OTP");
        }

        var requestOtpHash =
                SecurityHelper.HashOtp(request.Otp);

        if (requestOtpHash != otp.OtpCode)
        {
            user.FailedAttempts++;

            if (user.FailedAttempts >= 5)
            {
                user.LockUntil =
                    DateTime.Now.AddMinutes(15);
            }

            _db.LoginLogs.Add(new LoginLog
                        {
                            Id = Guid.NewGuid(),
                            UserId = user.Id,
                            SessionId = Guid.Empty,
                            LoginTime = DateTime.Now,
                            IpAddress =
                                HttpContext.Connection.RemoteIpAddress?.ToString(),
                            Browser =
                                Request.Headers.UserAgent.ToString(),
                            Action = "LOGIN_FAILED",
                            IsSuccess = false
                        });

            await _db.SaveChangesAsync();

            return BadRequest("Invalid OTP");
        }


        if (otp.ExpireDate < DateTime.Now)
        {
            return BadRequest("Expired OTP");
        }

        otp.IsUsed = true;
        user.FailedAttempts = 0;
        user.LockUntil = null;

        var session = new UserSession
                {
                    Id = Guid.NewGuid(),

                    UserId = user.Id,

                    Status = SessionStatus.Active,

                    LoginTime = DateTime.Now,

                    LastActivityTime = DateTime.Now,

                    IsActive = true,

                    IpAddress =
                        HttpContext.Connection.RemoteIpAddress?.ToString(),

                    Browser =
                        Request.Headers.UserAgent.ToString()
                };

        _db.UserSessions.Add(session);

        _db.LoginLogs.Add(new LoginLog
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    SessionId = session.Id,
                    LoginTime = DateTime.Now,
                    IpAddress =
                        HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Browser =
                        Request.Headers.UserAgent.ToString(),
                    Action = "LOGIN_SUCCESS",
                    IsSuccess = true
                });

        var accessToken =
            _jwtService.GenerateAccessToken(_userProfileDto);

        var refreshToken =
            SecurityHelper.GenerateRefreshToken();

        _db.RefreshTokens.Add(
                new RefreshToken
                {
                    Id = Guid.NewGuid(),

                            UserId = user.Id,

                            SessionId = session.Id,

                            TokenHash = SecurityHelper.HashToken(refreshToken),

                            RotationStatus = RefreshTokenStatus.Active,

                            ExpiryDate = DateTime.Now.AddDays(7),

                            CreatedDate = DateTime.Now,

                            IsRevoked = false
                });



        await _db.SaveChangesAsync();

        return Ok(new
        {
            accessToken,
            refreshToken,
            email = user.Email,
            fullName = user.FullName,
            role = user.Role
        });
    }

/// <summary>
/// Đoạn này chính là Refresh Token
/// 
/// </summary>
/// 
/// Cân phân biệt
/// Request DTO  ==> Dùng để nhận dữ liệu từ Frontend ==> public class RefreshTokenRequestDto {public string RefreshToken { get; set; } = string.Empty;}
/// Response DTO ==> Dùng để trả kết quả về Frontend: ==> public class TokenResponseDto {public string AccessToken { get; set; } = string.Empty;public string RefreshToken { get; set; } = string.Empty;}
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
        [FromBody] RefreshTokenRequestDto request)
    {
        var tokenHash = SecurityHelper.HashToken(
                request.RefreshToken);

        var oldToken =
            await _db.RefreshTokens
                .FirstOrDefaultAsync(x =>
                    x.TokenHash == tokenHash);

        if (oldToken == null)
        {
            return Unauthorized(
                "Refresh token invalid");
        }

        // hết hạn

        if (oldToken.ExpiryDate < DateTime.Now)
        {
            oldToken.RotationStatus =
                RefreshTokenStatus.Expired;

            await _db.SaveChangesAsync();

            return Unauthorized(
                "Refresh token expired");
        }

        // đã revoke

        if (oldToken.IsRevoked)
        {
            return Unauthorized(
                "Refresh token revoked");
        }

        // replay attack

        if (oldToken.RotationStatus ==
            RefreshTokenStatus.Rotated)
        {
            oldToken.RotationStatus =
                RefreshTokenStatus.ReuseDetected;

            oldToken.IsRevoked = true;

            oldToken.RevokedDate =
                DateTime.Now;

            // revoke toàn bộ token của session

            var familyTokens =
                await _db.RefreshTokens
                    .Where(x =>
                        x.SessionId ==
                        oldToken.SessionId)
                    .ToListAsync();

            foreach (var item in familyTokens)
            {
                item.IsRevoked = true;

                item.RevokedDate =
                    DateTime.Now;

                item.RotationStatus =
                    RefreshTokenStatus.Revoked;
            }

            await _db.SaveChangesAsync();

            return Unauthorized(
                "Token reuse detected");
        }

        var user =
            await _db.Users
                .FirstOrDefaultAsync(x =>
                    x.Id == oldToken.UserId);
        
        if (user == null)
        {
            return Unauthorized();
        }
        
        var _userProfileDto =
            new UserMeProfileDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role
            };

        // JWT mới

        var accessToken =
            _jwtService.GenerateAccessToken(_userProfileDto);

        // Refresh mới

        var newRefreshTokenRaw =
            SecurityHelper.GenerateRefreshToken();

        var newRefreshToken =
            new RefreshToken
            {
                Id = Guid.NewGuid(),

                UserId = user.Id,

                SessionId =
                    oldToken.SessionId,

                ParentTokenId =
                    oldToken.Id,

                TokenHash =
                    SecurityHelper.HashToken(
                        newRefreshTokenRaw),

                RotationStatus =
                    RefreshTokenStatus.Active,

                CreatedDate =
                    DateTime.Now,

                ExpiryDate =
                    DateTime.Now.AddDays(7),

                IsRevoked = false
            };

        // rotate token cũ

        oldToken.IsRevoked = true;

        oldToken.RevokedDate =
            DateTime.Now;

        oldToken.ReplacedByTokenId =
            newRefreshToken.Id;

        oldToken.RotationStatus =
            RefreshTokenStatus.Rotated;

        _db.RefreshTokens.Add(
            newRefreshToken);

        _db.LoginLogs.Add(
                new LoginLog
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    // SessionId = oldToken.Id,
                    SessionId = oldToken.SessionId,
                    LoginTime = DateTime.Now,
                    IpAddress =
                        HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Browser =
                        Request.Headers.UserAgent.ToString(),
                    // Action = "TOKEN_REUSE_DETECTED",
                    Action = "REFRESH_TOKEN",
                    IsSuccess = true
                });

        await _db.SaveChangesAsync();

        return Ok(new
        {
            accessToken,
            refreshToken = newRefreshTokenRaw
        });
    }
    

    /// <summary>
    /// Lấy thông tin người dùng hiện tại từ JWT
    /// Me()
    /// </summary>
    /// <returns></returns>
    /// 
    /// 
    
    [Authorize(
    AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {

         var userIdClaim =
        User.FindFirst(
            ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(
            userIdClaim,
            out Guid userId))
            {
                return Unauthorized();
            }

        var profile =
            await _userService.GetProfileAsync(
                userId);

        if (profile == null)
            {
                return NotFound();
            }

        return Ok(profile);
    
    }
    
    
    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        return Ok(new
        {
            Message = "JWT OK"
        });
    }
}

