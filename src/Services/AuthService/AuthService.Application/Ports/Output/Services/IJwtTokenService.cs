using AuthService.Domain.Entities;

namespace AuthService.Application.Ports.Output.Services;

public interface IJwtTokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
