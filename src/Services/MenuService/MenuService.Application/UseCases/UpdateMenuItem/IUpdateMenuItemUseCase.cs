using MenuService.Application.DTOs;

namespace MenuService.Application.UseCases.UpdateMenuItem;

public interface IUpdateMenuItemUseCase
{
    Task<MenuItemDto> ExecuteAsync(Guid id, UpdateMenuItemRequestDto request, CancellationToken ct = default);
}
