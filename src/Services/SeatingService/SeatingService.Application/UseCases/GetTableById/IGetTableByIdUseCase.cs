using SeatingService.Application.DTOs;

namespace SeatingService.Application.UseCases.GetTableById;

public interface IGetTableByIdUseCase
{
    Task<TableDto> ExecuteAsync(Guid id, CancellationToken ct = default);
}
