using FluentValidation;
using OrderService.Application.DTOs;
using OrderService.Domain.Entities;
using OrderService.Domain.Ports.Repositories;

namespace OrderService.Application.UseCases.Orders.UpdateItemQuantity;

public class UpdateItemQuantityUseCase : IUpdateItemQuantityUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IValidator<UpdateItemQuantityRequestDto> _validator;

    public UpdateItemQuantityUseCase(IOrderRepository orderRepository, IValidator<UpdateItemQuantityRequestDto> validator)
    {
        _orderRepository = orderRepository;
        _validator = validator;
    }

    public async Task<OrderDto> ExecuteAsync(Guid orderId, Guid itemId, UpdateItemQuantityRequestDto request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
        order.UpdateItemQuantity(itemId, request.Quantity);
        await _orderRepository.UpdateAsync(order, cancellationToken);

        return ToDto(order);
    }

    private static OrderDto ToDto(Order o) => new(
        o.Id, o.OrderNumber, o.TableId, o.WaiterId, o.Status.ToString(), o.Notes, o.Total,
        o.Items.Select(i => new OrderItemDto(i.Id, i.MenuItemId, i.MenuItemName, i.UnitPrice, i.Quantity, i.Subtotal, i.Notes)),
        o.CreatedAt, o.UpdatedAt, o.SentToKitchenAt, o.CompletedAt);
}
