namespace OrderService.Application.DTOs;

public record CreateOrderRequestDto(
    Guid TableId,
    Guid WaiterId,
    string? Notes,
    IEnumerable<CreateOrderItemDto> Items
);
