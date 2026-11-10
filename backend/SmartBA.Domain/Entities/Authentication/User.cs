// namespace SmartBA.Domain.Entities.Authentication;

namespace SmartBA.Domain.Entities.Authentication;
public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = "Customer";
    public string? AvatarUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

        // Security
    public int FailedAttempts { get; set; }

    public DateTime? LockUntil { get; set; }
}