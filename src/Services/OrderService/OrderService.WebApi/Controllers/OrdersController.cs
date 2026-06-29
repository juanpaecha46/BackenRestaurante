using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs;
using OrderService.Application.UseCases.Orders.AddItemToOrder;
using OrderService.Application.UseCases.Orders.CreateOrder;
using OrderService.Application.UseCases.Orders.GetOrderById;
using OrderService.Application.UseCases.Orders.GetOrdersByStatus;
using OrderService.Application.UseCases.Orders.GetOrdersByTable;
using OrderService.Application.UseCases.Orders.RemoveItemFromOrder;
using OrderService.Application.UseCases.Orders.SendToKitchen;
using OrderService.Application.UseCases.Orders.UpdateItemQuantity;
using OrderService.Application.UseCases.Orders.UpdateOrderStatus;
using OrderService.Domain.Enums;

namespace OrderService.WebApi.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly ICreateOrderUseCase _createOrder;
    private readonly IAddItemToOrderUseCase _addItem;
    private readonly IRemoveItemFromOrderUseCase _removeItem;
    private readonly IUpdateItemQuantityUseCase _updateQuantity;
    private readonly ISendToKitchenUseCase _sendToKitchen;
    private readonly IUpdateOrderStatusUseCase _updateStatus;
    private readonly IGetOrderByIdUseCase _getById;
    private readonly IGetOrdersByTableUseCase _getByTable;
    private readonly IGetOrdersByStatusUseCase _getByStatus;

    public OrdersController(
        ICreateOrderUseCase createOrder,
        IAddItemToOrderUseCase addItem,
        IRemoveItemFromOrderUseCase removeItem,
        IUpdateItemQuantityUseCase updateQuantity,
        ISendToKitchenUseCase sendToKitchen,
        IUpdateOrderStatusUseCase updateStatus,
        IGetOrderByIdUseCase getById,
        IGetOrdersByTableUseCase getByTable,
        IGetOrdersByStatusUseCase getByStatus)
    {
        _createOrder = createOrder;
        _addItem = addItem;
        _removeItem = removeItem;
        _updateQuantity = updateQuantity;
        _sendToKitchen = sendToKitchen;
        _updateStatus = updateStatus;
        _getById = getById;
        _getByTable = getByTable;
        _getByStatus = getByStatus;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> GetById(Guid id, CancellationToken ct)
    {
        var order = await _getById.ExecuteAsync(id, ct);
        return Ok(order);
    }

    [HttpGet("table/{tableId:guid}")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetByTable(Guid tableId, CancellationToken ct)
    {
        var orders = await _getByTable.ExecuteAsync(tableId, ct);
        return Ok(orders);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetByStatus(OrderStatus status, CancellationToken ct)
    {
        var orders = await _getByStatus.ExecuteAsync(status, ct);
        return Ok(orders);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderRequestDto request, CancellationToken ct)
    {
        var order = await _createOrder.ExecuteAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPost("{id:guid}/items")]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<OrderDto>> AddItem(Guid id, [FromBody] AddOrderItemRequestDto request, CancellationToken ct)
    {
        var order = await _addItem.ExecuteAsync(id, request, ct);
        return Ok(order);
    }

    [HttpDelete("{id:guid}/items/{itemId:guid}")]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<OrderDto>> RemoveItem(Guid id, Guid itemId, CancellationToken ct)
    {
        var order = await _removeItem.ExecuteAsync(id, itemId, ct);
        return Ok(order);
    }

    [HttpPatch("{id:guid}/items/{itemId:guid}/quantity")]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<OrderDto>> UpdateItemQuantity(Guid id, Guid itemId, [FromBody] UpdateItemQuantityRequestDto request, CancellationToken ct)
    {
        var order = await _updateQuantity.ExecuteAsync(id, itemId, request, ct);
        return Ok(order);
    }

    [HttpPatch("{id:guid}/send-to-kitchen")]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<OrderDto>> SendToKitchen(Guid id, CancellationToken ct)
    {
        var order = await _sendToKitchen.ExecuteAsync(id, ct);
        return Ok(order);
    }

    [HttpPatch("{id:guid}/status")]
    [Authorize(Roles = "Admin,Kitchen,Cashier")]
    public async Task<ActionResult<OrderDto>> UpdateStatus(Guid id, [FromBody] UpdateOrderStatusRequestDto request, CancellationToken ct)
    {
        var order = await _updateStatus.ExecuteAsync(id, request, ct);
        return Ok(order);
    }
}
