using AuthService.Application.DTOs;

namespace AuthService.Application.UseCases.Auth.RefreshToken;

public interface IRefreshTokenUseCase
{
    Task<LoginResponseDto> ExecuteAsync(RefreshTokenRequestDto request, CancellationToken ct = default);
}
