using Microsoft.EntityFrameworkCore;
using SeatingService.Domain.Entities;
using SeatingService.Domain.Enums;
using SeatingService.Domain.Ports.Repositories;

namespace SeatingService.Infrastructure.Persistence.Repositories;

public class TableRepository : ITableRepository
{
    private readonly AppDbContext _context;

    public TableRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Table?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _context.Tables.FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<Table?> GetByNumberAsync(string number, CancellationToken ct = default)
        => await _context.Tables.FirstOrDefaultAsync(t => t.Number == number, ct);

    public async Task<IReadOnlyList<Table>> GetAllAsync(TableStatus? status = null, CancellationToken ct = default)
    {
        var query = _context.Tables.AsQueryable();
        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);
        return await query.OrderBy(t => t.Number).ToListAsync(ct);
    }

    public async Task AddAsync(Table table, CancellationToken ct = default)
    {
        await _context.Tables.AddAsync(table, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Table table, CancellationToken ct = default)
    {
        _context.Tables.Update(table);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Table table, CancellationToken ct = default)
    {
        _context.Tables.Remove(table);
        await _context.SaveChangesAsync(ct);
    }
}
