using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.UseCases.Orders.AddItemToOrder;
using OrderService.Application.UseCases.Orders.CreateOrder;
using OrderService.Application.UseCases.Orders.GetOrderById;
using OrderService.Application.UseCases.Orders.GetOrdersByStatus;
using OrderService.Application.UseCases.Orders.GetOrdersByTable;
using OrderService.Application.UseCases.Orders.RemoveItemFromOrder;
using OrderService.Application.UseCases.Orders.SendToKitchen;
using OrderService.Application.UseCases.Orders.UpdateItemQuantity;
using OrderService.Application.UseCases.Orders.UpdateOrderStatus;

namespace OrderService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
        services.AddScoped<IAddItemToOrderUseCase, AddItemToOrderUseCase>();
        services.AddScoped<IRemoveItemFromOrderUseCase, RemoveItemFromOrderUseCase>();
        services.AddScoped<IUpdateItemQuantityUseCase, UpdateItemQuantityUseCase>();
        services.AddScoped<ISendToKitchenUseCase, SendToKitchenUseCase>();
        services.AddScoped<IUpdateOrderStatusUseCase, UpdateOrderStatusUseCase>();
        services.AddScoped<IGetOrderByIdUseCase, GetOrderByIdUseCase>();
        services.AddScoped<IGetOrdersByTableUseCase, GetOrdersByTableUseCase>();
        services.AddScoped<IGetOrdersByStatusUseCase, GetOrdersByStatusUseCase>();

        return services;
    }
}
