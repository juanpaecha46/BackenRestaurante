using FluentValidation;
using OrderService.Application.DTOs;
using OrderService.Domain.Entities;
using OrderService.Domain.Ports.Repositories;

namespace OrderService.Application.UseCases.Orders.CreateOrder;

public class CreateOrderUseCase : ICreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IValidator<CreateOrderRequestDto> _validator;

    public CreateOrderUseCase(IOrderRepository orderRepository, IValidator<CreateOrderRequestDto> validator)
    {
        _orderRepository = orderRepository;
        _validator = validator;
    }

    public async Task<OrderDto> ExecuteAsync(CreateOrderRequestDto request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var orderNumber = await _orderRepository.GetNextOrderNumberAsync(cancellationToken);
        var order = Order.Create(orderNumber, request.TableId, request.WaiterId, request.Notes);

        foreach (var item in request.Items)
            order.AddItem(item.MenuItemId, item.MenuItemName, item.UnitPrice, item.Quantity, item.Notes);

        await _orderRepository.AddAsync(order, cancellationToken);

        return ToDto(order);
    }

    private static OrderDto ToDto(Order o) => new(
        o.Id, o.OrderNumber, o.TableId, o.WaiterId, o.Status.ToString(), o.Notes, o.Total,
        o.Items.Select(i => new OrderItemDto(i.Id, i.MenuItemId, i.MenuItemName, i.UnitPrice, i.Quantity, i.Subtotal, i.Notes)),
        o.CreatedAt, o.UpdatedAt, o.SentToKitchenAt, o.CompletedAt);
}
