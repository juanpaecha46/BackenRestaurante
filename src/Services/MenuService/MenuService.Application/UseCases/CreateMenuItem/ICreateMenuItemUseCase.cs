using MenuService.Application.DTOs;

namespace MenuService.Application.UseCases.CreateMenuItem;

public interface ICreateMenuItemUseCase
{
    Task<MenuItemDto> ExecuteAsync(CreateMenuItemRequestDto request, CancellationToken ct = default);
}
