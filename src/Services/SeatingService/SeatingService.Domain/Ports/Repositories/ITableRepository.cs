using SeatingService.Domain.Entities;
using SeatingService.Domain.Enums;

namespace SeatingService.Domain.Ports.Repositories;

public interface ITableRepository
{
    Task<Table?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Table?> GetByNumberAsync(string number, CancellationToken ct = default);
    Task<IReadOnlyList<Table>> GetAllAsync(TableStatus? status = null, CancellationToken ct = default);
    Task AddAsync(Table table, CancellationToken ct = default);
    Task UpdateAsync(Table table, CancellationToken ct = default);
    Task DeleteAsync(Table table, CancellationToken ct = default);
}
