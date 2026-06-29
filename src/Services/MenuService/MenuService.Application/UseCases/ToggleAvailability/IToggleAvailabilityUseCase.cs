using MenuService.Application.DTOs;

namespace MenuService.Application.UseCases.ToggleAvailability;

public interface IToggleAvailabilityUseCase
{
    Task<MenuItemDto> ExecuteAsync(Guid id, CancellationToken ct = default);
}
