namespace OrderService.Application.DTOs;

public record AddOrderItemRequestDto(
    Guid MenuItemId,
    string MenuItemName,
    decimal UnitPrice,
    int Quantity,
    string? Notes
);
