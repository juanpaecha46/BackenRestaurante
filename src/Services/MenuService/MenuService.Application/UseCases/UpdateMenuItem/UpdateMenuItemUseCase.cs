using FluentValidation;
using MenuService.Application.DTOs;
using MenuService.Domain.Exceptions;
using MenuService.Domain.Ports.Repositories;

namespace MenuService.Application.UseCases.UpdateMenuItem;

public class UpdateMenuItemUseCase : IUpdateMenuItemUseCase
{
    private readonly IMenuItemRepository _repository;
    private readonly IValidator<UpdateMenuItemRequestDto> _validator;

    public UpdateMenuItemUseCase(IMenuItemRepository repository, IValidator<UpdateMenuItemRequestDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<MenuItemDto> ExecuteAsync(Guid id, UpdateMenuItemRequestDto request, CancellationToken ct = default)
    {
        await _validator.ValidateAndThrowAsync(request, ct);

        var item = await _repository.GetByIdAsync(id, ct)
            ?? throw new MenuItemNotFoundException(id);

        item.Update(request.Name, request.Description, request.Price, request.Category, request.ImageUrl);
        await _repository.UpdateAsync(item, ct);

        return new MenuItemDto(
            item.Id, item.Name, item.Description, item.Price,
            item.Category.ToString(), item.Type.ToString(),
            item.IsAvailable, item.ImageUrl, item.CreatedAt, item.UpdatedAt
        );
    }
}
