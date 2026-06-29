namespace OrderService.Domain.Exceptions;

public class OrderNotFoundException : Exception
{
    public OrderNotFoundException(Guid id) : base($"Pedido {id} no encontrado.") { }
}
