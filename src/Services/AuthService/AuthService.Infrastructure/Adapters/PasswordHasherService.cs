using AuthService.Application.Ports.Output.Services;

namespace AuthService.Infrastructure.Adapters;

public class PasswordHasherService : IPasswordHasher
{
    public string Hash(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, string hash) =>
        BCrypt.Net.BCrypt.Verify(password, hash);
}
