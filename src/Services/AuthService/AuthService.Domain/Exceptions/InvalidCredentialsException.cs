namespace AuthService.Domain.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException() : base("Credenciales inválidas.") { }
}
