namespace OrderService.Domain.Exceptions;

public class OrderItemNotFoundException : Exception
{
    public OrderItemNotFoundException(Guid id) : base($"Ítem {id} no encontrado en el pedido.") { }
}
