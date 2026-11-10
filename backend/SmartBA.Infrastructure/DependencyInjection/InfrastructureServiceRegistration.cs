using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartBA.Infrastructure.Persistence;
using SmartBA.Application.DTOs.Authentication;
using SmartBA.Application.Interfaces.Email;
using SmartBA.Application.Interfaces.Authentication;
using SmartBA.Infrastructure.Services.Authentication;
using SmartBA.Infrastructure.Services.User;
using SmartBA.Infrastructure.Services.Email;

namespace SmartBA.Infrastructure.Services;


public static class InfrastructureServiceRegistration
{
    public static IServiceCollection
        AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        
        services.AddScoped<IUserMeService,
            UserMeService>();

        services.AddScoped<IOtpService,
            OtpService>();

        services.AddScoped<IUserSessionService,
            UserSessionService>();

        services.AddScoped<IRefreshTokenService,
            RefreshTokenService>();

        services.AddScoped<ILoginLogService,
            LoginLogService>();

        services.AddScoped<IAuthService,
            AuthService>();

        services.AddDbContext<ApplicationDbContext>();

        services.AddScoped<IEmailService,
            EmailService>();

        services.AddScoped<JwtService>();

        return services;
    }
}   