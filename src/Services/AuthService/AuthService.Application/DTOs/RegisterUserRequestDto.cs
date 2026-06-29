using AuthService.Domain.Enums;

namespace AuthService.Application.DTOs;

public record RegisterUserRequestDto(
    string Name,
    string Email,
    string Password,
    UserRole Role
);
