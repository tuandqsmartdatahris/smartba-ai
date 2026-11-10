namespace SmartBA.Domain.Entities.Authentication;
using SmartBA.Domain.Enums;
public class UserSession
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public SessionStatus Status { get; set; } = SessionStatus.Active;

    public DateTime LoginTime { get; set; }

    public DateTime? LogoutTime { get; set; }

    public DateTime? LastActivityTime { get; set; }

    public string? IpAddress { get; set; }

    public string? Browser { get; set; }

    public bool? IsActive { get; set; }
}