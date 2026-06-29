using MenuService.Application.DTOs;

namespace MenuService.Application.UseCases.GetMenuItemById;

public interface IGetMenuItemByIdUseCase
{
    Task<MenuItemDto> ExecuteAsync(Guid id, CancellationToken ct = default);
}
