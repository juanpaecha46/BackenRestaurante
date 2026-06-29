namespace AuthService.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(Guid id) : base($"Usuario {id} no encontrado.") { }
    public UserNotFoundException(string email) : base($"Usuario con email '{email}' no encontrado.") { }
}
