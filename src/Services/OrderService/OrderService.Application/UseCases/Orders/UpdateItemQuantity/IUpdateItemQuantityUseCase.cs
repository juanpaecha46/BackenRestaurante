using OrderService.Application.DTOs;

namespace OrderService.Application.UseCases.Orders.UpdateItemQuantity;

public interface IUpdateItemQuantityUseCase
{
    Task<OrderDto> ExecuteAsync(Guid orderId, Guid itemId, UpdateItemQuantityRequestDto request, CancellationToken cancellationToken = default);
}
