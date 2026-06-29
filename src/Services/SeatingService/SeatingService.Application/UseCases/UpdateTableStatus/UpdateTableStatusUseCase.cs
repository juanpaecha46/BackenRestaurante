using SeatingService.Application.DTOs;
using SeatingService.Domain.Exceptions;
using SeatingService.Domain.Ports.Repositories;

namespace SeatingService.Application.UseCases.UpdateTableStatus;

public class UpdateTableStatusUseCase : IUpdateTableStatusUseCase
{
    private readonly ITableRepository _repository;

    public UpdateTableStatusUseCase(ITableRepository repository)
    {
        _repository = repository;
    }

    public async Task<TableDto> ExecuteAsync(Guid tableId, UpdateTableStatusRequestDto request, CancellationToken ct = default)
    {
        var table = await _repository.GetByIdAsync(tableId, ct)
            ?? throw new TableNotFoundException(tableId);

        table.UpdateStatus(request.Status);
        await _repository.UpdateAsync(table, ct);
        return table.ToDto();
    }
}
