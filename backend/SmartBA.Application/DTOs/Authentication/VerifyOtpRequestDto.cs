
namespace SmartBA.Application.DTOs.Authentication;

public class VerifyOtpRequestDto
{
    public string Email { get; set; } = "";

    public string Otp { get; set; } = "";
}