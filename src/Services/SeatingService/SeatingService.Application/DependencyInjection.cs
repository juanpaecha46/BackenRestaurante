using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SeatingService.Application.UseCases.AssignOrder;
using SeatingService.Application.UseCases.CreateTable;
using SeatingService.Application.UseCases.DeleteTable;
using SeatingService.Application.UseCases.GetAllTables;
using SeatingService.Application.UseCases.GetTableById;
using SeatingService.Application.UseCases.ReleaseTable;
using SeatingService.Application.UseCases.UpdateTable;
using SeatingService.Application.UseCases.UpdateTableStatus;

namespace SeatingService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddScoped<ICreateTableUseCase, CreateTableUseCase>();
        services.AddScoped<IUpdateTableUseCase, UpdateTableUseCase>();
        services.AddScoped<IDeleteTableUseCase, DeleteTableUseCase>();
        services.AddScoped<IGetTableByIdUseCase, GetTableByIdUseCase>();
        services.AddScoped<IGetAllTablesUseCase, GetAllTablesUseCase>();
        services.AddScoped<IAssignOrderUseCase, AssignOrderUseCase>();
        services.AddScoped<IReleaseTableUseCase, ReleaseTableUseCase>();
        services.AddScoped<IUpdateTableStatusUseCase, UpdateTableStatusUseCase>();

        return services;
    }
}
