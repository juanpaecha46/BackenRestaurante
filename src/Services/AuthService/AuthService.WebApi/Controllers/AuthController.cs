using AuthService.Application.DTOs;
using AuthService.Application.UseCases.Auth.Login;
using AuthService.Application.UseCases.Auth.RefreshToken;
using AuthService.Application.UseCases.Auth.RegisterUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginUseCase _loginUseCase;
    private readonly IRegisterUserUseCase _registerUserUseCase;
    private readonly IRefreshTokenUseCase _refreshTokenUseCase;

    public AuthController(
        ILoginUseCase loginUseCase,
        IRegisterUserUseCase registerUserUseCase,
        IRefreshTokenUseCase refreshTokenUseCase)
    {
        _loginUseCase = loginUseCase;
        _registerUserUseCase = registerUserUseCase;
        _refreshTokenUseCase = refreshTokenUseCase;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken ct)
    {
        var result = await _loginUseCase.ExecuteAsync(request, ct);
        return Ok(result);
    }

    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto request, CancellationToken ct)
    {
        var result = await _registerUserUseCase.ExecuteAsync(request, ct);
        return StatusCode(201, result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request, CancellationToken ct)
    {
        var result = await _refreshTokenUseCase.ExecuteAsync(request, ct);
        return Ok(result);
    }
}
