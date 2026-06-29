using AuthService.Application.DTOs;

namespace AuthService.Application.UseCases.Auth.RegisterUser;

public interface IRegisterUserUseCase
{
    Task<UserDto> ExecuteAsync(RegisterUserRequestDto request, CancellationToken ct = default);
}
