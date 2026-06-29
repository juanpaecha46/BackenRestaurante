using SeatingService.Domain.Enums;

namespace SeatingService.Application.DTOs;

public record TableDto(
    Guid Id,
    string Number,
    int Capacity,
    TableStatus Status,
    string StatusName,
    string? Location,
    Guid? CurrentOrderId,
    DateTime CreatedAt,
    DateTime UpdatedAt);
