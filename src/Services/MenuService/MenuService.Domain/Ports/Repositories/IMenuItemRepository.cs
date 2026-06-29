using MenuService.Domain.Entities;
using MenuService.Domain.Enums;

namespace MenuService.Domain.Ports.Repositories;

public interface IMenuItemRepository
{
    Task<MenuItem?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<MenuItem>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<MenuItem>> GetByCategoryAsync(MenuItemCategory category, CancellationToken ct = default);
    Task<IEnumerable<MenuItem>> GetAvailableAsync(CancellationToken ct = default);
    Task AddAsync(MenuItem item, CancellationToken ct = default);
    Task UpdateAsync(MenuItem item, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
