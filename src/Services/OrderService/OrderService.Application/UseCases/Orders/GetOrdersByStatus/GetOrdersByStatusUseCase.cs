using OrderService.Application.DTOs;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Domain.Ports.Repositories;

namespace OrderService.Application.UseCases.Orders.GetOrdersByStatus;

public class GetOrdersByStatusUseCase : IGetOrdersByStatusUseCase
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersByStatusUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderDto>> ExecuteAsync(OrderStatus status, CancellationToken cancellationToken = default)
    {
        var orders = await _orderRepository.GetByStatusAsync(status, cancellationToken);
        return orders.Select(ToDto);
    }

    private static OrderDto ToDto(Order o) => new(
        o.Id, o.OrderNumber, o.TableId, o.WaiterId, o.Status.ToString(), o.Notes, o.Total,
        o.Items.Select(i => new OrderItemDto(i.Id, i.MenuItemId, i.MenuItemName, i.UnitPrice, i.Quantity, i.Subtotal, i.Notes)),
        o.CreatedAt, o.UpdatedAt, o.SentToKitchenAt, o.CompletedAt);
}
