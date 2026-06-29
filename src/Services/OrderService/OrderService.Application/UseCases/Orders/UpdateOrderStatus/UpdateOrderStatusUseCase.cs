using OrderService.Application.DTOs;
using OrderService.Domain.Entities;
using OrderService.Domain.Ports.Repositories;

namespace OrderService.Application.UseCases.Orders.UpdateOrderStatus;

public class UpdateOrderStatusUseCase : IUpdateOrderStatusUseCase
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderStatusUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto> ExecuteAsync(Guid orderId, UpdateOrderStatusRequestDto request, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
        order.UpdateStatus(request.Status);
        await _orderRepository.UpdateAsync(order, cancellationToken);

        return ToDto(order);
    }

    private static OrderDto ToDto(Order o) => new(
        o.Id, o.OrderNumber, o.TableId, o.WaiterId, o.Status.ToString(), o.Notes, o.Total,
        o.Items.Select(i => new OrderItemDto(i.Id, i.MenuItemId, i.MenuItemName, i.UnitPrice, i.Quantity, i.Subtotal, i.Notes)),
        o.CreatedAt, o.UpdatedAt, o.SentToKitchenAt, o.CompletedAt);
}
