namespace SmartBA.Application.Interfaces.Security;

public interface ISecurityEventService
{
    Task LogAsync(
        Guid? userId,
        Guid? sessionId,
        string eventType,
        string severity,
        string? description);
}