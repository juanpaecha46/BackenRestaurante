using AuthService.Application.DTOs;
using AuthService.Application.Ports.Output.Services;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Ports.Repositories;
using FluentValidation;

namespace AuthService.Application.UseCases.Auth.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IValidator<LoginRequestDto> _validator;

    public LoginUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        IValidator<LoginRequestDto> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _validator = validator;
    }

    public async Task<LoginResponseDto> ExecuteAsync(LoginRequestDto request, CancellationToken ct = default)
    {
        await _validator.ValidateAndThrowAsync(request, ct);

        var user = await _userRepository.GetByEmailAsync(request.Email, ct)
            ?? throw new InvalidCredentialsException();

        if (!user.IsActive || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new InvalidCredentialsException();

        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var refreshToken = _jwtTokenService.GenerateRefreshToken();

        user.SetRefreshToken(refreshToken, DateTime.UtcNow.AddDays(7));
        await _userRepository.UpdateAsync(user, ct);

        return new LoginResponseDto(
            accessToken,
            refreshToken,
            DateTime.UtcNow.AddMinutes(15),
            user.Role.ToString(),
            user.Name
        );
    }
}
