namespace SeatingService.Application.UseCases.DeleteTable;

public interface IDeleteTableUseCase
{
    Task ExecuteAsync(Guid id, CancellationToken ct = default);
}
