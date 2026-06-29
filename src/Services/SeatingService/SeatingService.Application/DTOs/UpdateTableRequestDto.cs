namespace SeatingService.Application.DTOs;

public record UpdateTableRequestDto(
    string Number,
    int Capacity,
    string? Location);
