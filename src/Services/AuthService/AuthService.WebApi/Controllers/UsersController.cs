using AuthService.Application.UseCases.Users.GetAllUsers;
using AuthService.Application.UseCases.Users.GetUserById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly IGetAllUsersUseCase _getAllUsersUseCase;
    private readonly IGetUserByIdUseCase _getUserByIdUseCase;

    public UsersController(IGetAllUsersUseCase getAllUsersUseCase, IGetUserByIdUseCase getUserByIdUseCase)
    {
        _getAllUsersUseCase = getAllUsersUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _getAllUsersUseCase.ExecuteAsync(ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _getUserByIdUseCase.ExecuteAsync(id, ct);
        return Ok(result);
    }
}
