using System.Reflection;
using FluentValidation;
using MenuService.Application.UseCases.CreateMenuItem;
using MenuService.Application.UseCases.DeleteMenuItem;
using MenuService.Application.UseCases.GetAllMenuItems;
using MenuService.Application.UseCases.GetMenuItemById;
using MenuService.Application.UseCases.ToggleAvailability;
using MenuService.Application.UseCases.UpdateMenuItem;
using Microsoft.Extensions.DependencyInjection;

namespace MenuService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<ICreateMenuItemUseCase, CreateMenuItemUseCase>();
        services.AddScoped<IUpdateMenuItemUseCase, UpdateMenuItemUseCase>();
        services.AddScoped<IDeleteMenuItemUseCase, DeleteMenuItemUseCase>();
        services.AddScoped<IGetMenuItemByIdUseCase, GetMenuItemByIdUseCase>();
        services.AddScoped<IGetAllMenuItemsUseCase, GetAllMenuItemsUseCase>();
        services.AddScoped<IToggleAvailabilityUseCase, ToggleAvailabilityUseCase>();

        return services;
    }
}
