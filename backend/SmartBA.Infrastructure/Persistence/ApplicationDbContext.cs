using Microsoft.EntityFrameworkCore;
using SmartBA.Domain.Entities.Auth;
using SmartBA.Domain.Entities.project;
using SmartBA.Application.DTOs.Auth;

namespace SmartBA.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<SecurityEvent> SecurityEvents => Set<SecurityEvent>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<UserOtp> UserOtps => Set<UserOtp>();
    public DbSet<UserSession> UserSessions => Set<UserSession>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<LoginLog> LoginLogs => Set<LoginLog>();
    public DbSet<UserProfileDto> Users => Set<UserProfileDto>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("smartba");

        modelBuilder.Entity<UserProfileDto>().ToTable("Users", "system");
        modelBuilder.Entity<UserOtp>().ToTable("UserOtps", "system");
        modelBuilder.Entity<RefreshToken>().ToTable("RefreshTokens", "system");
        modelBuilder.Entity<LoginLog>().ToTable("LoginLogs", "system");
        modelBuilder.Entity<UserSession>().ToTable("UserSessions", "system");

        modelBuilder.Entity<UserSession>()
                .Property(x => x.Status)
                .HasConversion<string>();

        modelBuilder.Entity<RefreshToken>()
                .Property(x => x.RotationStatus)
                .HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }
}