using OrderService.Application.DTOs;

namespace OrderService.Application.UseCases.Orders.SendToKitchen;

public interface ISendToKitchenUseCase
{
    Task<OrderDto> ExecuteAsync(Guid orderId, CancellationToken cancellationToken = default);
}
