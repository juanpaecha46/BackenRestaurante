using OrderService.Application.DTOs;

namespace OrderService.Application.UseCases.Orders.CreateOrder;

public interface ICreateOrderUseCase
{
    Task<OrderDto> ExecuteAsync(CreateOrderRequestDto request, CancellationToken cancellationToken = default);
}
