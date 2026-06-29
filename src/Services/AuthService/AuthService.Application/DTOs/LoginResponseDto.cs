namespace AuthService.Application.DTOs;

public record LoginResponseDto(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt,
    string Role,
    string Name
);
