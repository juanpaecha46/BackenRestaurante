using AuthService.Application.DTOs;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Ports.Repositories;

namespace AuthService.Application.UseCases.Users.GetUserById;

public class GetUserByIdUseCase : IGetUserByIdUseCase
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdUseCase(IUserRepository userRepository) =>
        _userRepository = userRepository;

    public async Task<UserDto> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(id, ct)
            ?? throw new UserNotFoundException(id);

        return new UserDto(user.Id, user.Name, user.Email, user.Role.ToString(), user.IsActive, user.CreatedAt);
    }
}
