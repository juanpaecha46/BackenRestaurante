using SeatingService.Application.DTOs;

namespace SeatingService.Application.UseCases.UpdateTableStatus;

public interface IUpdateTableStatusUseCase
{
    Task<TableDto> ExecuteAsync(Guid tableId, UpdateTableStatusRequestDto request, CancellationToken ct = default);
}
