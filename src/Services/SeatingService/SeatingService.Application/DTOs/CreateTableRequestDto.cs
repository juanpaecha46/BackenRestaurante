namespace SeatingService.Application.DTOs;

public record CreateTableRequestDto(
    string Number,
    int Capacity,
    string? Location);
