using SeatingService.Application.DTOs;

namespace SeatingService.Application.UseCases.ReleaseTable;

public interface IReleaseTableUseCase
{
    Task<TableDto> ExecuteAsync(Guid tableId, CancellationToken ct = default);
}
