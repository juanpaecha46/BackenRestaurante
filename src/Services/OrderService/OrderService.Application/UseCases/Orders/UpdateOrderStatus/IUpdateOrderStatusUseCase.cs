using OrderService.Application.DTOs;

namespace OrderService.Application.UseCases.Orders.UpdateOrderStatus;

public interface IUpdateOrderStatusUseCase
{
    Task<OrderDto> ExecuteAsync(Guid orderId, UpdateOrderStatusRequestDto request, CancellationToken cancellationToken = default);
}
