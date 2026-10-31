using Microsoft.AspNetCore.Http;
using SmartBA.Application.Interfaces.Security;
using SmartBA.Infrastructure.Persistence;
using SmartBA.Application.Interfaces;
using SmartBA.Domain.Entities;
using SmartBA.Domain.Entities.Auth;

namespace SmartBA.Infrastructure.Services.Security;

public class SecurityEventService : ISecurityEventService
{
    private readonly ApplicationDbContext  _db;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SecurityEventService(
        ApplicationDbContext  db,
        IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task LogAsync(
        Guid? userId,
        Guid? sessionId,
        string eventType,
        string severity,
        string? description)
    {
        var httpContext =
            _httpContextAccessor.HttpContext;

        var securityEvent = new SecurityEvent
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            SessionId = sessionId,
            EventType = eventType,
            Severity = severity,
            Description = description,

            IpAddress =
                httpContext?
                    .Connection
                    .RemoteIpAddress?
                    .ToString(),

            UserAgent =
                httpContext?
                    .Request
                    .Headers["User-Agent"]
                    .ToString(),

            EventDate = DateTime.UtcNow,

            IsResolved = false
        };

        _db.SecurityEvents.Add(securityEvent);

        await _db.SaveChangesAsync();
    }
}