using SeatingService.Application.DTOs;
using SeatingService.Domain.Exceptions;
using SeatingService.Domain.Ports.Repositories;

namespace SeatingService.Application.UseCases.GetTableById;

public class GetTableByIdUseCase : IGetTableByIdUseCase
{
    private readonly ITableRepository _repository;

    public GetTableByIdUseCase(ITableRepository repository)
    {
        _repository = repository;
    }

    public async Task<TableDto> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var table = await _repository.GetByIdAsync(id, ct)
            ?? throw new TableNotFoundException(id);

        return table.ToDto();
    }
}
