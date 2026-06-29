using MenuService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenuService.Infrastructure.Persistence.Configurations;

public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(m => m.Price)
            .IsRequired()
            .HasColumnType("numeric(10,2)");

        builder.Property(m => m.Category)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(m => m.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(m => m.IsAvailable)
            .IsRequired();

        builder.Property(m => m.ImageUrl)
            .HasMaxLength(500);

        builder.Property(m => m.CreatedAt)
            .IsRequired();
    }
}
