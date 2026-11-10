namespace SmartBA.Domain.Entities.Authentication;

public class LoginLog
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid SessionId { get; set; }

    public DateTime LoginTime { get; set; }

    public DateTime? LogoutTime { get; set; }

    public string? LogoutReason { get; set; }

    public string? IpAddress { get; set; }

    public string? Browser { get; set; }

    public string? OperatingSystem { get; set; }
    
    public string? Action { get; set; }

    public bool IsSuccess { get; set; }
}