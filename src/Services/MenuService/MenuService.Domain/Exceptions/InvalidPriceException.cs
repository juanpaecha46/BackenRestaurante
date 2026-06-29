namespace MenuService.Domain.Exceptions;

public class InvalidPriceException : Exception
{
    public InvalidPriceException(decimal price) : base($"El precio '{price}' no es válido. Debe ser mayor a cero.") { }
}
