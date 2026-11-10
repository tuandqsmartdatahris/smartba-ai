using SmartBA.Domain.Entities.Authentication;
namespace SmartBA.Domain.Entities.Authentication;
public class SecurityEvent
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public Guid? SessionId { get; set; }

    public string EventType { get; set; } = string.Empty;

    public string Severity { get; set; } = "Info";

    public string? Description { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime EventDate { get; set; }

    public bool IsResolved { get; set; }
}