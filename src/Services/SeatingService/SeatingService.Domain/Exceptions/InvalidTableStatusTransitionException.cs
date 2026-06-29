using SeatingService.Domain.Enums;

namespace SeatingService.Domain.Exceptions;

public class InvalidTableStatusTransitionException : Exception
{
    public InvalidTableStatusTransitionException(TableStatus current, TableStatus target)
        : base($"Transición de estado inválida: '{current}' → '{target}'.") { }
}
