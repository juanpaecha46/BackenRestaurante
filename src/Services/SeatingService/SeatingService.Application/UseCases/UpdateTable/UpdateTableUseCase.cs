using FluentValidation;
using SeatingService.Application.DTOs;
using SeatingService.Domain.Exceptions;
using SeatingService.Domain.Ports.Repositories;

namespace SeatingService.Application.UseCases.UpdateTable;

public class UpdateTableUseCase : IUpdateTableUseCase
{
    private readonly ITableRepository _repository;
    private readonly IValidator<UpdateTableRequestDto> _validator;

    public UpdateTableUseCase(ITableRepository repository, IValidator<UpdateTableRequestDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<TableDto> ExecuteAsync(Guid id, UpdateTableRequestDto request, CancellationToken ct = default)
    {
        await _validator.ValidateAndThrowAsync(request, ct);

        var table = await _repository.GetByIdAsync(id, ct)
            ?? throw new TableNotFoundException(id);

        if (table.Number != request.Number)
        {
            var existing = await _repository.GetByNumberAsync(request.Number, ct);
            if (existing is not null)
                throw new TableNumberAlreadyExistsException(request.Number);
        }

        table.Update(request.Number, request.Capacity, request.Location);
        await _repository.UpdateAsync(table, ct);
        return table.ToDto();
    }
}
