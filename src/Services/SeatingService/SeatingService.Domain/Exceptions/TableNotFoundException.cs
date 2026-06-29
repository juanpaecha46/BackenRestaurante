namespace SeatingService.Domain.Exceptions;

public class TableNotFoundException : Exception
{
    public TableNotFoundException(Guid id)
        : base($"La mesa con Id '{id}' no fue encontrada.") { }
}
