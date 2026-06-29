using OrderService.Domain.Enums;

namespace OrderService.Domain.Exceptions;

public class InvalidOrderStatusTransitionException : Exception
{
    public InvalidOrderStatusTransitionException(OrderStatus from, OrderStatus to)
        : base($"No se puede cambiar el estado de '{from}' a '{to}'.") { }

    public InvalidOrderStatusTransitionException(string message) : base(message) { }
}
