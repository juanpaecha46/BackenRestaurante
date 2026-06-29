namespace OrderService.Application.DTOs;

public record OrderItemDto(
    Guid Id,
    Guid MenuItemId,
    string MenuItemName,
    decimal UnitPrice,
    int Quantity,
    decimal Subtotal,
    string? Notes
);
