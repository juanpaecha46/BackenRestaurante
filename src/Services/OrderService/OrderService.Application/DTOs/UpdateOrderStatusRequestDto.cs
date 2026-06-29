using OrderService.Domain.Enums;

namespace OrderService.Application.DTOs;

public record UpdateOrderStatusRequestDto(OrderStatus Status);
