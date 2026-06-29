using MenuService.Domain.Exceptions;
using MenuService.Domain.Ports.Repositories;

namespace MenuService.Application.UseCases.DeleteMenuItem;

public class DeleteMenuItemUseCase : IDeleteMenuItemUseCase
{
    private readonly IMenuItemRepository _repository;

    public DeleteMenuItemUseCase(IMenuItemRepository repository) => _repository = repository;

    public async Task ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var item = await _repository.GetByIdAsync(id, ct)
            ?? throw new MenuItemNotFoundException(id);

        await _repository.DeleteAsync(item.Id, ct);
    }
}
