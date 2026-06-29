using SeatingService.Application.DTOs;
using SeatingService.Domain.Exceptions;
using SeatingService.Domain.Ports.Repositories;

namespace SeatingService.Application.UseCases.ReleaseTable;

public class ReleaseTableUseCase : IReleaseTableUseCase
{
    private readonly ITableRepository _repository;

    public ReleaseTableUseCase(ITableRepository repository)
    {
        _repository = repository;
    }

    public async Task<TableDto> ExecuteAsync(Guid tableId, CancellationToken ct = default)
    {
        var table = await _repository.GetByIdAsync(tableId, ct)
            ?? throw new TableNotFoundException(tableId);

        table.Release();
        await _repository.UpdateAsync(table, ct);
        return table.ToDto();
    }
}
