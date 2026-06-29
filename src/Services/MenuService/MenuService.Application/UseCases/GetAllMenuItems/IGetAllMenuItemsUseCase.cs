using MenuService.Application.DTOs;
using MenuService.Domain.Enums;

namespace MenuService.Application.UseCases.GetAllMenuItems;

public interface IGetAllMenuItemsUseCase
{
    Task<IEnumerable<MenuItemDto>> ExecuteAsync(
        MenuItemCategory? category = null,
        bool? onlyAvailable = null,
        CancellationToken ct = default);
}
