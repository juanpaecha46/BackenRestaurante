using MenuService.Application.DTOs;
using MenuService.Application.UseCases.CreateMenuItem;
using MenuService.Application.UseCases.DeleteMenuItem;
using MenuService.Application.UseCases.GetAllMenuItems;
using MenuService.Application.UseCases.GetMenuItemById;
using MenuService.Application.UseCases.ToggleAvailability;
using MenuService.Application.UseCases.UpdateMenuItem;
using MenuService.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuService.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IGetAllMenuItemsUseCase _getAllMenuItems;
    private readonly IGetMenuItemByIdUseCase _getMenuItemById;
    private readonly ICreateMenuItemUseCase _createMenuItem;
    private readonly IUpdateMenuItemUseCase _updateMenuItem;
    private readonly IDeleteMenuItemUseCase _deleteMenuItem;
    private readonly IToggleAvailabilityUseCase _toggleAvailability;

    public MenuController(
        IGetAllMenuItemsUseCase getAllMenuItems,
        IGetMenuItemByIdUseCase getMenuItemById,
        ICreateMenuItemUseCase createMenuItem,
        IUpdateMenuItemUseCase updateMenuItem,
        IDeleteMenuItemUseCase deleteMenuItem,
        IToggleAvailabilityUseCase toggleAvailability)
    {
        _getAllMenuItems = getAllMenuItems;
        _getMenuItemById = getMenuItemById;
        _createMenuItem = createMenuItem;
        _updateMenuItem = updateMenuItem;
        _deleteMenuItem = deleteMenuItem;
        _toggleAvailability = toggleAvailability;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] MenuItemCategory? category,
        [FromQuery] bool? onlyAvailable,
        CancellationToken ct)
    {
        var result = await _getAllMenuItems.ExecuteAsync(category, onlyAvailable, ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _getMenuItemById.ExecuteAsync(id, ct);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateMenuItemRequestDto request, CancellationToken ct)
    {
        var result = await _createMenuItem.ExecuteAsync(request, ct);
        return StatusCode(201, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMenuItemRequestDto request, CancellationToken ct)
    {
        var result = await _updateMenuItem.ExecuteAsync(id, request, ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _deleteMenuItem.ExecuteAsync(id, ct);
        return NoContent();
    }

    [HttpPatch("{id:guid}/toggle-availability")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ToggleAvailability(Guid id, CancellationToken ct)
    {
        var result = await _toggleAvailability.ExecuteAsync(id, ct);
        return Ok(result);
    }
}
