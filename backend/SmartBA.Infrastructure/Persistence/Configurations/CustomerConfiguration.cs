using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartBA.Domain.Entities.Customers;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers", "Project");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Masothue)
              .IsRequired()
              .IsUnicode()
              .HasMaxLength(10);

        builder.HasIndex(e => e.Masothue)
              .IsUnique();          

        // Cấu hình các cột khác tại đây...
    }
}