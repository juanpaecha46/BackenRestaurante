using AuthService.Application.DTOs;

namespace AuthService.Application.UseCases.Users.GetUserById;

public interface IGetUserByIdUseCase
{
    Task<UserDto> ExecuteAsync(Guid id, CancellationToken ct = default);
}
