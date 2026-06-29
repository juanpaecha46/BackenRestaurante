using SeatingService.Application.DTOs;

namespace SeatingService.Application.UseCases.UpdateTable;

public interface IUpdateTableUseCase
{
    Task<TableDto> ExecuteAsync(Guid id, UpdateTableRequestDto request, CancellationToken ct = default);
}
