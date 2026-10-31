namespace SmartBA.Domain.Enums;

public enum LogoutReason
{
    UserLogout = 1,

    SessionTimeout = 2,

    TokenExpired = 3,

    BrowserClosed = 4,

    ForceLogout = 5
}