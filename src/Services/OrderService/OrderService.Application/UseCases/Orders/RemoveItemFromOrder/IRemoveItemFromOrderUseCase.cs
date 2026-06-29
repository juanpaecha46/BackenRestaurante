using OrderService.Application.DTOs;

namespace OrderService.Application.UseCases.Orders.RemoveItemFromOrder;

public interface IRemoveItemFromOrderUseCase
{
    Task<OrderDto> ExecuteAsync(Guid orderId, Guid itemId, CancellationToken cancellationToken = default);
}
