using SeatingService.Application.DTOs;

namespace SeatingService.Application.UseCases.CreateTable;

public interface ICreateTableUseCase
{
    Task<TableDto> ExecuteAsync(CreateTableRequestDto request, CancellationToken ct = default);
}
