using OrderService.Domain.Exceptions;

namespace OrderService.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid MenuItemId { get; private set; }
    public string MenuItemName { get; private set; } = default!;
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public string? Notes { get; private set; }
    public decimal Subtotal => UnitPrice * Quantity;

    private OrderItem() { }

    public static OrderItem Create(
        Guid orderId,
        Guid menuItemId,
        string menuItemName,
        decimal unitPrice,
        int quantity,
        string? notes = null)
    {
        if (quantity <= 0) throw new InvalidQuantityException(quantity);

        return new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            MenuItemId = menuItemId,
            MenuItemName = menuItemName,
            UnitPrice = unitPrice,
            Quantity = quantity,
            Notes = notes
        };
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0) throw new InvalidQuantityException(quantity);
        Quantity = quantity;
    }
}
