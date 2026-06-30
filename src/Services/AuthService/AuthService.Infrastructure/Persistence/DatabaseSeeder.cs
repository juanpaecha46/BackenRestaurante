using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuthService.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static async Task SeedAdminAsync(AppDbContext context, ILogger logger)
    {
        if (await context.Users.AnyAsync())
            return;

        logger.LogInformation("No hay usuarios registrados. Creando administrador inicial...");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!");
        var admin = User.Create("Administrador", "admin@restaurante.com", passwordHash, UserRole.Admin);

        context.Users.Add(admin);
        await context.SaveChangesAsync();

        logger.LogInformation("Administrador creado — Email: admin@restaurante.com | Contraseña: Admin123!");
    }
}
