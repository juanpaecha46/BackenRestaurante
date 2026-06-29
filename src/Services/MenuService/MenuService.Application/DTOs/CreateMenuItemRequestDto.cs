using MenuService.Domain.Enums;

namespace MenuService.Application.DTOs;

public record CreateMenuItemRequestDto(
    string Name,
    string Description,
    decimal Price,
    MenuItemCategory Category,
    MenuItemType Type,
    string? ImageUrl
);
