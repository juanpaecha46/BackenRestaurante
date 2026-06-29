namespace MenuService.Application.DTOs;

public record MenuItemDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Category,
    string Type,
    bool IsAvailable,
    string? ImageUrl,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
