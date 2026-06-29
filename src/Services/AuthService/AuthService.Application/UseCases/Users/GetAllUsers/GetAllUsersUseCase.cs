using AuthService.Application.DTOs;
using AuthService.Domain.Ports.Repositories;

namespace AuthService.Application.UseCases.Users.GetAllUsers;

public class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersUseCase(IUserRepository userRepository) =>
        _userRepository = userRepository;

    public async Task<IEnumerable<UserDto>> ExecuteAsync(CancellationToken ct = default)
    {
        var users = await _userRepository.GetAllAsync(ct);
        return users.Select(u => new UserDto(u.Id, u.Name, u.Email, u.Role.ToString(), u.IsActive, u.CreatedAt));
    }
}
