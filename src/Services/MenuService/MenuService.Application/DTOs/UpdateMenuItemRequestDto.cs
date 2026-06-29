using MenuService.Domain.Enums;

namespace MenuService.Application.DTOs;

public record UpdateMenuItemRequestDto(
    string Name,
    string Description,
    decimal Price,
    MenuItemCategory Category,
    string? ImageUrl
);
