using Microsoft.EntityFrameworkCore;
using SmartBA.Domain.Entities.Customers;
using SmartBA.Domain.Entities.Authentication;
using System.Reflection;

namespace SmartBA.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<SecurityEvent> SecurityEvents => Set<SecurityEvent>();
    public DbSet<UserOtp> UserOtps => Set<UserOtp>();
    public DbSet<UserSession> UserSessions => Set<UserSession>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<LoginLog> LoginLogs => Set<LoginLog>();
    public DbSet<User> Users => Set<User>();
  

    // Add DbSet for other entities here...

    public DbSet<Customer> Customers => Set<Customer>();

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.Now;
                if (entry.Entity.Id == Guid.Empty)
                {
                    entry.Entity.Id = Guid.NewGuid();
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedDate = DateTime.Now;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("smartba");

        modelBuilder.Entity<User>().ToTable("Users", "system");
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

        // TUANDQ: Customer

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}