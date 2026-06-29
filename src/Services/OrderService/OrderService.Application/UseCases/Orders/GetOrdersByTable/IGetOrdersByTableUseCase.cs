using OrderService.Application.DTOs;

namespace OrderService.Application.UseCases.Orders.GetOrdersByTable;

public interface IGetOrdersByTableUseCase
{
    Task<IEnumerable<OrderDto>> ExecuteAsync(Guid tableId, CancellationToken cancellationToken = default);
}
