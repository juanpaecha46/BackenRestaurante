using FluentValidation;
using SeatingService.Application.DTOs;
using SeatingService.Domain.Exceptions;
using SeatingService.Domain.Ports.Repositories;

namespace SeatingService.Application.UseCases.AssignOrder;

public class AssignOrderUseCase : IAssignOrderUseCase
{
    private readonly ITableRepository _repository;
    private readonly IValidator<AssignOrderRequestDto> _validator;

    public AssignOrderUseCase(ITableRepository repository, IValidator<AssignOrderRequestDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<TableDto> ExecuteAsync(Guid tableId, AssignOrderRequestDto request, CancellationToken ct = default)
    {
        await _validator.ValidateAndThrowAsync(request, ct);

        var table = await _repository.GetByIdAsync(tableId, ct)
            ?? throw new TableNotFoundException(tableId);

        table.AssignOrder(request.OrderId);
        await _repository.UpdateAsync(table, ct);
        return table.ToDto();
    }
}
