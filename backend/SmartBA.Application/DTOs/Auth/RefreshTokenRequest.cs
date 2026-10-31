namespace SmartBA.Application.DTOs.Auth;

using SmartBA.Domain.Enums;

public class RefreshTokenRequest
{
   /* public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string? TokenHash { get; set; } 

    public Guid? ParentTokenId { get; set; }
    public Guid? ReplacedByTokenId { get; set; }

    public string? RotationStatus { get; set; } 

    public DateTime ExpiryDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? RevokedDate { get; set; }

    public bool IsRevoked { get; set; } */

    public string RefreshToken { get; set; } = string.Empty;
}

