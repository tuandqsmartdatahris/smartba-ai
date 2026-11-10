namespace SmartBA.Domain.Enums;

public enum SecurityEventType
{
    LoginSuccess = 1,

    LoginFailed = 2,

    OtpFailed = 3,

    AccountLocked = 4,

    RefreshTokenUsed = 5,

    RefreshTokenExpired = 6,

    RefreshTokenReuseDetected = 7,

    SessionRevoked = 8,

    UserDisabled = 9
}