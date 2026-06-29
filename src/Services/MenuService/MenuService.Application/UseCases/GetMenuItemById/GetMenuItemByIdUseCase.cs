using MenuService.Application.DTOs;
using MenuService.Domain.Exceptions;
using MenuService.Domain.Ports.Repositories;

namespace MenuService.Application.UseCases.GetMenuItemById;

public class GetMenuItemByIdUseCase : IGetMenuItemByIdUseCase
{
    private readonly IMenuItemRepository _repository;

    public GetMenuItemByIdUseCase(IMenuItemRepository repository) => _repository = repository;

    public async Task<MenuItemDto> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var item = await _repository.GetByIdAsync(id, ct)
            ?? throw new MenuItemNotFoundException(id);

        return new MenuItemDto(
            item.Id, item.Name, item.Description, item.Price,
            item.Category.ToString(), item.Type.ToString(),
            item.IsAvailable, item.ImageUrl, item.CreatedAt, item.UpdatedAt
        );
    }
}
