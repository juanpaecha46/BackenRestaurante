using SeatingService.Domain.Exceptions;
using SeatingService.Domain.Ports.Repositories;

namespace SeatingService.Application.UseCases.DeleteTable;

public class DeleteTableUseCase : IDeleteTableUseCase
{
    private readonly ITableRepository _repository;

    public DeleteTableUseCase(ITableRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var table = await _repository.GetByIdAsync(id, ct)
            ?? throw new TableNotFoundException(id);

        await _repository.DeleteAsync(table, ct);
    }
}
