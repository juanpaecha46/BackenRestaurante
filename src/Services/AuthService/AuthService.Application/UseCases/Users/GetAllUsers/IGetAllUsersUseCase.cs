using AuthService.Application.DTOs;

namespace AuthService.Application.UseCases.Users.GetAllUsers;

public interface IGetAllUsersUseCase
{
    Task<IEnumerable<UserDto>> ExecuteAsync(CancellationToken ct = default);
}
