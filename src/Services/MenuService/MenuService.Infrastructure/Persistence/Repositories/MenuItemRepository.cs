using MenuService.Domain.Entities;
using MenuService.Domain.Enums;
using MenuService.Domain.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MenuService.Infrastructure.Persistence.Repositories;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly AppDbContext _context;

    public MenuItemRepository(AppDbContext context) => _context = context;

    public async Task<MenuItem?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _context.MenuItems.FindAsync(new object[] { id }, ct);

    public async Task<IEnumerable<MenuItem>> GetAllAsync(CancellationToken ct = default) =>
        await _context.MenuItems.OrderBy(m => m.Category).ThenBy(m => m.Name).ToListAsync(ct);

    public async Task<IEnumerable<MenuItem>> GetByCategoryAsync(MenuItemCategory category, CancellationToken ct = default) =>
        await _context.MenuItems.Where(m => m.Category == category).OrderBy(m => m.Name).ToListAsync(ct);

    public async Task<IEnumerable<MenuItem>> GetAvailableAsync(CancellationToken ct = default) =>
        await _context.MenuItems.Where(m => m.IsAvailable).OrderBy(m => m.Category).ThenBy(m => m.Name).ToListAsync(ct);

    public async Task AddAsync(MenuItem item, CancellationToken ct = default)
    {
        await _context.MenuItems.AddAsync(item, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(MenuItem item, CancellationToken ct = default)
    {
        _context.MenuItems.Update(item);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var item = await _context.MenuItems.FindAsync(new object[] { id }, ct);
        if (item is not null)
        {
            _context.MenuItems.Remove(item);
            await _context.SaveChangesAsync(ct);
        }
    }
}
