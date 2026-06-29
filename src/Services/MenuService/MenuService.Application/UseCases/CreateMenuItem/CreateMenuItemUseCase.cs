using FluentValidation;
using MenuService.Application.DTOs;
using MenuService.Domain.Entities;
using MenuService.Domain.Ports.Repositories;

namespace MenuService.Application.UseCases.CreateMenuItem;

public class CreateMenuItemUseCase : ICreateMenuItemUseCase
{
    private readonly IMenuItemRepository _repository;
    private readonly IValidator<CreateMenuItemRequestDto> _validator;

    public CreateMenuItemUseCase(IMenuItemRepository repository, IValidator<CreateMenuItemRequestDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<MenuItemDto> ExecuteAsync(CreateMenuItemRequestDto request, CancellationToken ct = default)
    {
        await _validator.ValidateAndThrowAsync(request, ct);

        var item = MenuItem.Create(
            request.Name,
            request.Description,
            request.Price,
            request.Category,
            request.Type,
            request.ImageUrl
        );

        await _repository.AddAsync(item, ct);

        return ToDto(item);
    }

    private static MenuItemDto ToDto(MenuItem item) => new(
        item.Id, item.Name, item.Description, item.Price,
        item.Category.ToString(), item.Type.ToString(),
        item.IsAvailable, item.ImageUrl, item.CreatedAt, item.UpdatedAt
    );
}
