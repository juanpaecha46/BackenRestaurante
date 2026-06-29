using OrderService.Domain.Enums;
using OrderService.Domain.Exceptions;

namespace OrderService.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public int OrderNumber { get; private set; }
    public Guid TableId { get; private set; }
    public Guid WaiterId { get; private set; }
    public OrderStatus Status { get; private set; }
    public string? Notes { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? SentToKitchenAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public decimal Total => _items.Sum(i => i.Subtotal);

    private Order() { }

    public static Order Create(int orderNumber, Guid tableId, Guid waiterId, string? notes = null)
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = orderNumber,
            TableId = tableId,
            WaiterId = waiterId,
            Status = OrderStatus.Pending,
            Notes = notes,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void AddItem(Guid menuItemId, string menuItemName, decimal unitPrice, int quantity, string? notes = null)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOrderStatusTransitionException(
                $"Solo se pueden agregar ítems a pedidos en estado Pendiente. Estado actual: {Status}.");

        var existing = _items.FirstOrDefault(i => i.MenuItemId == menuItemId);
        if (existing is not null)
            existing.UpdateQuantity(existing.Quantity + quantity);
        else
            _items.Add(OrderItem.Create(Id, menuItemId, menuItemName, unitPrice, quantity, notes));

        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveItem(Guid orderItemId)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOrderStatusTransitionException(
                $"Solo se pueden eliminar ítems de pedidos en estado Pendiente. Estado actual: {Status}.");

        var item = _items.FirstOrDefault(i => i.Id == orderItemId)
            ?? throw new OrderItemNotFoundException(orderItemId);

        _items.Remove(item);
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateItemQuantity(Guid orderItemId, int quantity)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOrderStatusTransitionException(
                $"Solo se pueden modificar ítems de pedidos en estado Pendiente. Estado actual: {Status}.");

        var item = _items.FirstOrDefault(i => i.Id == orderItemId)
            ?? throw new OrderItemNotFoundException(orderItemId);

        item.UpdateQuantity(quantity);
        UpdatedAt = DateTime.UtcNow;
    }

    public void SendToKitchen()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOrderStatusTransitionException(OrderStatus.Pending, OrderStatus.SentToKitchen);
        if (!_items.Any())
            throw new EmptyOrderException();

        Status = OrderStatus.SentToKitchen;
        SentToKitchenAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(OrderStatus newStatus)
    {
        ValidateTransition(newStatus);
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
        if (newStatus is OrderStatus.Delivered or OrderStatus.Cancelled)
            CompletedAt = DateTime.UtcNow;
    }

    private void ValidateTransition(OrderStatus newStatus)
    {
        var valid = Status switch
        {
            OrderStatus.Pending       => new[] { OrderStatus.SentToKitchen, OrderStatus.Cancelled },
            OrderStatus.SentToKitchen => new[] { OrderStatus.InPreparation, OrderStatus.Cancelled },
            OrderStatus.InPreparation => new[] { OrderStatus.Ready },
            OrderStatus.Ready         => new[] { OrderStatus.Delivered },
            _                         => Array.Empty<OrderStatus>()
        };

        if (!valid.Contains(newStatus))
            throw new InvalidOrderStatusTransitionException(Status, newStatus);
    }
}
