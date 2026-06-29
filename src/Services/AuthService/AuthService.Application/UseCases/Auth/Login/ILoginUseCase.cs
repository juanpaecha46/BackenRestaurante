using AuthService.Application.DTOs;

namespace AuthService.Application.UseCases.Auth.Login;

public interface ILoginUseCase
{
    Task<LoginResponseDto> ExecuteAsync(LoginRequestDto request, CancellationToken ct = default);
}
