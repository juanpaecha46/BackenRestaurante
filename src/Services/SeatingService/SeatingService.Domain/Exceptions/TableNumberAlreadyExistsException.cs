namespace SeatingService.Domain.Exceptions;

public class TableNumberAlreadyExistsException : Exception
{
    public TableNumberAlreadyExistsException(string number)
        : base($"Ya existe una mesa con el número '{number}'.") { }
}
