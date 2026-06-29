using MenuService.Application.DTOs;
using MenuService.Domain.Enums;
using MenuService.Domain.Ports.Repositories;

namespace MenuService.Application.UseCases.GetAllMenuItems;

public class GetAllMenuItemsUseCase : IGetAllMenuItemsUseCase
{
    private readonly IMenuItemRepository _repository;

    public GetAllMenuItemsUseCase(IMenuItemRepository repository) => _repository = repository;

    public async Task<IEnumerable<MenuItemDto>> ExecuteAsync(
        MenuItemCategory? category = null,
        bool? onlyAvailable = null,
        CancellationToken ct = default)
    {
        var items = category.HasValue
            ? await _repository.GetByCategoryAsync(category.Value, ct)
            : onlyAvailable == true
                ? await _repository.GetAvailableAsync(ct)
                : await _repository.GetAllAsync(ct);

        return items.Select(item => new MenuItemDto(
            item.Id, item.Name, item.Description, item.Price,
            item.Category.ToString(), item.Type.ToString(),
            item.IsAvailable, item.ImageUrl, item.CreatedAt, item.UpdatedAt
        ));
    }
}
