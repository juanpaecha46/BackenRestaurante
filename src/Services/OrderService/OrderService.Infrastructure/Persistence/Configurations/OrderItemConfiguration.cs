using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.MenuItemName).IsRequired().HasMaxLength(200);
        builder.Property(i => i.UnitPrice).HasColumnType("numeric(10,2)").IsRequired();
        builder.Property(i => i.Quantity).IsRequired();
        builder.Property(i => i.Notes).HasMaxLength(500);

        builder.Ignore(i => i.Subtotal);
    }
}
