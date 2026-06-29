using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatingService.Application.DTOs;
using SeatingService.Application.UseCases.AssignOrder;
using SeatingService.Application.UseCases.CreateTable;
using SeatingService.Application.UseCases.DeleteTable;
using SeatingService.Application.UseCases.GetAllTables;
using SeatingService.Application.UseCases.GetTableById;
using SeatingService.Application.UseCases.ReleaseTable;
using SeatingService.Application.UseCases.UpdateTable;
using SeatingService.Application.UseCases.UpdateTableStatus;
using SeatingService.Domain.Enums;

namespace SeatingService.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TablesController : ControllerBase
{
    private readonly IGetAllTablesUseCase _getAllTables;
    private readonly IGetTableByIdUseCase _getTableById;
    private readonly ICreateTableUseCase _createTable;
    private readonly IUpdateTableUseCase _updateTable;
    private readonly IDeleteTableUseCase _deleteTable;
    private readonly IAssignOrderUseCase _assignOrder;
    private readonly IReleaseTableUseCase _releaseTable;
    private readonly IUpdateTableStatusUseCase _updateTableStatus;

    public TablesController(
        IGetAllTablesUseCase getAllTables,
        IGetTableByIdUseCase getTableById,
        ICreateTableUseCase createTable,
        IUpdateTableUseCase updateTable,
        IDeleteTableUseCase deleteTable,
        IAssignOrderUseCase assignOrder,
        IReleaseTableUseCase releaseTable,
        IUpdateTableStatusUseCase updateTableStatus)
    {
        _getAllTables = getAllTables;
        _getTableById = getTableById;
        _createTable = createTable;
        _updateTable = updateTable;
        _deleteTable = deleteTable;
        _assignOrder = assignOrder;
        _releaseTable = releaseTable;
        _updateTableStatus = updateTableStatus;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] TableStatus? status, CancellationToken ct)
    {
        var result = await _getAllTables.ExecuteAsync(status, ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _getTableById.ExecuteAsync(id, ct);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateTableRequestDto request, CancellationToken ct)
    {
        var result = await _createTable.ExecuteAsync(request, ct);
        return StatusCode(201, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTableRequestDto request, CancellationToken ct)
    {
        var result = await _updateTable.ExecuteAsync(id, request, ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _deleteTable.ExecuteAsync(id, ct);
        return NoContent();
    }

    [HttpPost("{id:guid}/assign-order")]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<IActionResult> AssignOrder(Guid id, [FromBody] AssignOrderRequestDto request, CancellationToken ct)
    {
        var result = await _assignOrder.ExecuteAsync(id, request, ct);
        return Ok(result);
    }

    [HttpPost("{id:guid}/release")]
    [Authorize(Roles = "Admin,Waiter,Cashier")]
    public async Task<IActionResult> Release(Guid id, CancellationToken ct)
    {
        var result = await _releaseTable.ExecuteAsync(id, ct);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateTableStatusRequestDto request, CancellationToken ct)
    {
        var result = await _updateTableStatus.ExecuteAsync(id, request, ct);
        return Ok(result);
    }
}
