namespace SeatingService.Domain.Exceptions;

public class TableAlreadyOccupiedException : Exception
{
    public TableAlreadyOccupiedException(Guid tableId)
        : base($"La mesa '{tableId}' ya tiene un pedido asignado.") { }
}
