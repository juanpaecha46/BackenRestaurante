using FluentValidation;
using OrderService.Application.DTOs;
using OrderService.Domain.Entities;
using OrderService.Domain.Ports.Repositories;

namespace OrderService.Application.UseCases.Orders.AddItemToOrder;

public class AddItemToOrderUseCase : IAddItemToOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IValidator<AddOrderItemRequestDto> _validator;

    public AddItemToOrderUseCase(IOrderRepository orderRepository, IValidator<AddOrderItemRequestDto> validator)
    {
        _orderRepository = orderRepository;
        _validator = validator;
    }

    public async Task<OrderDto> ExecuteAsync(Guid orderId, AddOrderItemRequestDto request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
        order.AddItem(request.MenuItemId, request.MenuItemName, request.UnitPrice, request.Quantity, request.Notes);
        await _orderRepository.UpdateAsync(order, cancellationToken);

        return ToDto(order);
    }

    private static OrderDto ToDto(Order o) => new(
        o.Id, o.OrderNumber, o.TableId, o.WaiterId, o.Status.ToString(), o.Notes, o.Total,
        o.Items.Select(i => new OrderItemDto(i.Id, i.MenuItemId, i.MenuItemName, i.UnitPrice, i.Quantity, i.Subtotal, i.Notes)),
        o.CreatedAt, o.UpdatedAt, o.SentToKitchenAt, o.CompletedAt);
}
