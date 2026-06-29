using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeatingService.Domain.Ports.Repositories;
using SeatingService.Infrastructure.Persistence;
using SeatingService.Infrastructure.Persistence.Repositories;

namespace SeatingService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITableRepository, TableRepository>();

        return services;
    }
}
