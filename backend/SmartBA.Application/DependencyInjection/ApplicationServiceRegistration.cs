using Microsoft.Extensions.DependencyInjection;

using SmartBA.Application.Interfaces.Auth;
using SmartBA.Application.Interfaces.Security;

using SmartBA.Application.Services.Auth;

namespace SmartBA.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IUserService,
            UserService>();

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

        return services;
    }
}