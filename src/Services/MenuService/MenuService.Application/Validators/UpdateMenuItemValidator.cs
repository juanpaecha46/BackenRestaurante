using FluentValidation;
using MenuService.Application.DTOs;

namespace MenuService.Application.Validators;

public class UpdateMenuItemValidator : AbstractValidator<UpdateMenuItemRequestDto>
{
    public UpdateMenuItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MaximumLength(150).WithMessage("El nombre no puede superar 150 caracteres.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es requerida.")
            .MaximumLength(500).WithMessage("La descripción no puede superar 500 caracteres.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a cero.");

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("La categoría no es válida.");

        RuleFor(x => x.ImageUrl)
            .MaximumLength(500).WithMessage("La URL de imagen no puede superar 500 caracteres.")
            .When(x => x.ImageUrl is not null);
    }
}
