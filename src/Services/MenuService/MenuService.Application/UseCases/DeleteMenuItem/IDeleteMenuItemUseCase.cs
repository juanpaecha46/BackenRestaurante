namespace MenuService.Application.UseCases.DeleteMenuItem;

public interface IDeleteMenuItemUseCase
{
    Task ExecuteAsync(Guid id, CancellationToken ct = default);
}
