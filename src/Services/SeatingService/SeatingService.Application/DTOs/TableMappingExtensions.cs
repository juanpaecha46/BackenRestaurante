using SeatingService.Domain.Entities;

namespace SeatingService.Application.DTOs;

internal static class TableMappingExtensions
{
    internal static TableDto ToDto(this Table t) => new(
        t.Id,
        t.Number,
        t.Capacity,
        t.Status,
        t.Status.ToString(),
        t.Location,
        t.CurrentOrderId,
        t.CreatedAt,
        t.UpdatedAt);
}
