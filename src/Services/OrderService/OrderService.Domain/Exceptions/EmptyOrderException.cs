namespace OrderService.Domain.Exceptions;

public class EmptyOrderException : Exception
{
    public EmptyOrderException()
        : base("El pedido no tiene ítems. Agrega al menos uno antes de enviar a cocina.") { }
}
