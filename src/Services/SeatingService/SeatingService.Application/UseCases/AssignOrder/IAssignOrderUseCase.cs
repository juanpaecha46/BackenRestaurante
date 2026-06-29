using SeatingService.Application.DTOs;

namespace SeatingService.Application.UseCases.AssignOrder;

public interface IAssignOrderUseCase
{
    Task<TableDto> ExecuteAsync(Guid tableId, AssignOrderRequestDto request, CancellationToken ct = default);
}
