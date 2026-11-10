namespace SmartBA.Domain.Entities.Authentication;
using SmartBA.Domain.Enums;
public class RefreshToken
{
    public Guid Id { get; set; }

    public Guid SessionId { get; set; }
    public Guid UserId { get; set; }

    public string? TokenHash { get; set; } 

    public Guid? ParentTokenId { get; set; }
    public Guid? ReplacedByTokenId { get; set; }

    public RefreshTokenStatus  RotationStatus { get; set; } 

    public DateTime ExpiryDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? RevokedDate { get; set; }

    public bool IsRevoked { get; set; }
}
