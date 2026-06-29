namespace OrderService.Application.DTOs;

public record OrderDto(
    Guid Id,
    int OrderNumber,
    Guid TableId,
    Guid WaiterId,
    string Status,
    string? Notes,
    decimal Total,
    IEnumerable<OrderItemDto> Items,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? SentToKitchenAt,
    DateTime? CompletedAt
);
