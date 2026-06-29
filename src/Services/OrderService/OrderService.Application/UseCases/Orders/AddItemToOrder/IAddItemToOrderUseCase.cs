using OrderService.Application.DTOs;

namespace OrderService.Application.UseCases.Orders.AddItemToOrder;

public interface IAddItemToOrderUseCase
{
    Task<OrderDto> ExecuteAsync(Guid orderId, AddOrderItemRequestDto request, CancellationToken cancellationToken = default);
}
