namespace OrderService.Domain.Exceptions;

public class InvalidQuantityException : Exception
{
    public InvalidQuantityException(int quantity)
        : base($"La cantidad '{quantity}' no es válida. Debe ser mayor a cero.") { }
}
