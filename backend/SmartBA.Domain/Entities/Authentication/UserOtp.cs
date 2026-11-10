namespace SmartBA.Domain.Entities.Authentication;

public class UserOtp
{
    public Guid Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string OtpCode { get; set; } = string.Empty;

    public DateTime ExpireDate { get; set; }

    public bool IsUsed { get; set; }

    public DateTime CreatedDate { get; set; }
}