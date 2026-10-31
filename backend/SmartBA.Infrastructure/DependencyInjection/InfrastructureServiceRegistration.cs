using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartBA.Infrastructure.Persistence;
using SmartBA.Infrastructure.Services.Email;
using SmartBA.Application.Interfaces.Email;
namespace SmartBA.Infrastructure.Services.Authentication;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection
        AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>();

        services.AddScoped<IEmailService,
            EmailService>();

        services.AddScoped<JwtService>();

        return services;
    }
}   