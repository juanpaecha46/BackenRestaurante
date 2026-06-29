using OrderService.Application.DTOs;
using OrderService.Domain.Entities;
using OrderService.Domain.Ports.Repositories;

namespace OrderService.Application.UseCases.Orders.GetOrdersByTable;

public class GetOrdersByTableUseCase : IGetOrdersByTableUseCase
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersByTableUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderDto>> ExecuteAsync(Guid tableId, CancellationToken cancellationToken = default)
    {
        var orders = await _orderRepository.GetByTableIdAsync(tableId, cancellationToken);
        return orders.Select(ToDto);
    }

    private static OrderDto ToDto(Order o) => new(
        o.Id, o.OrderNumber, o.TableId, o.WaiterId, o.Status.ToString(), o.Notes, o.Total,
        o.Items.Select(i => new OrderItemDto(i.Id, i.MenuItemId, i.MenuItemName, i.UnitPrice, i.Quantity, i.Subtotal, i.Notes)),
        o.CreatedAt, o.UpdatedAt, o.SentToKitchenAt, o.CompletedAt);
}
