using SeatingService.Application.DTOs;
using SeatingService.Domain.Enums;

namespace SeatingService.Application.UseCases.GetAllTables;

public interface IGetAllTablesUseCase
{
    Task<IReadOnlyList<TableDto>> ExecuteAsync(TableStatus? status = null, CancellationToken ct = default);
}
