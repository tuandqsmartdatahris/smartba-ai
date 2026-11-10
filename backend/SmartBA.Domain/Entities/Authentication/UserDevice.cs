namespace SmartBA.Domain.Entities.Authentication;
public class UserDevice
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? DeviceName { get; set; }

    public string? Browser { get; set; }

    public string? IpAddress { get; set; }

    public DateTime LastSeen { get; set; }
}