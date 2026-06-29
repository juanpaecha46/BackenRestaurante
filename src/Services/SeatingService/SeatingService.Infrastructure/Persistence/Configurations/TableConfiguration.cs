using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatingService.Domain.Entities;
using SeatingService.Domain.Enums;

namespace SeatingService.Infrastructure.Persistence.Configurations;

public class TableConfiguration : IEntityTypeConfiguration<Table>
{
    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.ToTable("tables");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("id");

        builder.Property(t => t.Number)
            .HasColumnName("number")
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(t => t.Number).IsUnique();

        builder.Property(t => t.Capacity)
            .HasColumnName("capacity")
            .IsRequired();

        builder.Property(t => t.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(t => t.Location)
            .HasColumnName("location")
            .HasMaxLength(100);

        builder.Property(t => t.CurrentOrderId)
            .HasColumnName("current_order_id");

        builder.Property(t => t.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(t => t.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();
    }
}
