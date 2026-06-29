using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.Domain.Ports.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Order>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Order>> GetByTableIdAsync(Guid tableId, CancellationToken ct = default);
    Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status, CancellationToken ct = default);
    Task<IEnumerable<Order>> GetActiveOrdersAsync(CancellationToken ct = default);
    Task<int> GetNextOrderNumberAsync(CancellationToken ct = default);
    Task AddAsync(Order order, CancellationToken ct = default);
    Task UpdateAsync(Order order, CancellationToken ct = default);
}
