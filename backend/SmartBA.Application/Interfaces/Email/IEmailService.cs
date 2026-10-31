namespace SmartBA.Application.Interfaces.Email;
using System.Threading.Tasks;
public interface IEmailService
{
    Task SendOtpAsync(
        string email,
        string otp);
}