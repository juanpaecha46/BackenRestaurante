using AuthService.Application.DTOs;
using AuthService.Application.Ports.Output.Services;
using AuthService.Domain.Entities;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Ports.Repositories;
using FluentValidation;

namespace AuthService.Application.UseCases.Auth.RegisterUser;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IValidator<RegisterUserRequestDto> _validator;

    public RegisterUserUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IValidator<RegisterUserRequestDto> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _validator = validator;
    }

    public async Task<UserDto> ExecuteAsync(RegisterUserRequestDto request, CancellationToken ct = default)
    {
        await _validator.ValidateAndThrowAsync(request, ct);

        var existing = await _userRepository.GetByEmailAsync(request.Email, ct);
        if (existing is not null)
            throw new EmailAlreadyExistsException(request.Email);

        var passwordHash = _passwordHasher.Hash(request.Password);
        var user = User.Create(request.Name, request.Email, passwordHash, request.Role);

        await _userRepository.AddAsync(user, ct);

        return new UserDto(user.Id, user.Name, user.Email, user.Role.ToString(), user.IsActive, user.CreatedAt);
    }
}
