using FluentValidation;
using SeatingService.Application.DTOs;
using SeatingService.Domain.Entities;
using SeatingService.Domain.Exceptions;
using SeatingService.Domain.Ports.Repositories;

namespace SeatingService.Application.UseCases.CreateTable;

public class CreateTableUseCase : ICreateTableUseCase
{
    private readonly ITableRepository _repository;
    private readonly IValidator<CreateTableRequestDto> _validator;

    public CreateTableUseCase(ITableRepository repository, IValidator<CreateTableRequestDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<TableDto> ExecuteAsync(CreateTableRequestDto request, CancellationToken ct = default)
    {
        await _validator.ValidateAndThrowAsync(request, ct);

        var existing = await _repository.GetByNumberAsync(request.Number, ct);
        if (existing is not null)
            throw new TableNumberAlreadyExistsException(request.Number);

        var table = Table.Create(request.Number, request.Capacity, request.Location);
        await _repository.AddAsync(table, ct);
        return table.ToDto();
    }
}
