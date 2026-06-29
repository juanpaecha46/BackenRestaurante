namespace OrderService.Application.DTOs;

public record CreateOrderItemDto(
    Guid MenuItemId,
    string MenuItemName,
    decimal UnitPrice,
    int Quantity,
    string? Notes
);
