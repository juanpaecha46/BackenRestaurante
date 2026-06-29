using SeatingService.Application.DTOs;
using SeatingService.Domain.Enums;
using SeatingService.Domain.Ports.Repositories;

namespace SeatingService.Application.UseCases.GetAllTables;

public class GetAllTablesUseCase : IGetAllTablesUseCase
{
    private readonly ITableRepository _repository;

    public GetAllTablesUseCase(ITableRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TableDto>> ExecuteAsync(TableStatus? status = null, CancellationToken ct = default)
    {
        var tables = await _repository.GetAllAsync(status, ct);
        return tables.Select(t => t.ToDto()).ToList().AsReadOnly();
    }
}
