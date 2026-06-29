using System.Reflection;
using AuthService.Application.UseCases.Auth.Login;
using AuthService.Application.UseCases.Auth.RefreshToken;
using AuthService.Application.UseCases.Auth.RegisterUser;
using AuthService.Application.UseCases.Users.GetAllUsers;
using AuthService.Application.UseCases.Users.GetUserById;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();
        services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
        services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();

        return services;
    }
}
