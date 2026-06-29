using OrderService.Application.DTOs;

namespace OrderService.Application.UseCases.Orders.GetOrderById;

public interface IGetOrderByIdUseCase
{
    Task<OrderDto> ExecuteAsync(Guid orderId, CancellationToken cancellationToken = default);
}
