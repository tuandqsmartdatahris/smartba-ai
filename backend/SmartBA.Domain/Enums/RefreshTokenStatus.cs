namespace SmartBA.Domain.Enums;

public enum RefreshTokenStatus
{
    Active = 1,

    Rotated = 2,

    Revoked = 3,

    Expired = 4,

    ReuseDetected = 5
}