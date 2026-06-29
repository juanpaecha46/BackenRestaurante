using OrderService.Application.DTOs;
using OrderService.Domain.Enums;

namespace OrderService.Application.UseCases.Orders.GetOrdersByStatus;

public interface IGetOrdersByStatusUseCase
{
    Task<IEnumerable<OrderDto>> ExecuteAsync(OrderStatus status, CancellationToken cancellationToken = default);
}
