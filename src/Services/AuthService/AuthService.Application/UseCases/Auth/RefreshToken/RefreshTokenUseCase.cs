using AuthService.Application.DTOs;
using AuthService.Application.Ports.Output.Services;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Ports.Repositories;

namespace AuthService.Application.UseCases.Auth.RefreshToken;

public class RefreshTokenUseCase : IRefreshTokenUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public RefreshTokenUseCase(IUserRepository userRepository, IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponseDto> ExecuteAsync(RefreshTokenRequestDto request, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken, ct)
            ?? throw new InvalidCredentialsException();

        if (!user.IsRefreshTokenValid(request.RefreshToken))
            throw new InvalidCredentialsException();

        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

        user.SetRefreshToken(newRefreshToken, DateTime.UtcNow.AddDays(7));
        await _userRepository.UpdateAsync(user, ct);

        return new LoginResponseDto(
            accessToken,
            newRefreshToken,
            DateTime.UtcNow.AddMinutes(15),
            user.Role.ToString(),
            user.Name
        );
    }
}
