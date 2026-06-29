using FluentValidation;
using SeatingService.Application.DTOs;

namespace SeatingService.Application.Validators;

public class CreateTableValidator : AbstractValidator<CreateTableRequestDto>
{
    public CreateTableValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("El número de mesa es obligatorio.")
            .MaximumLength(10).WithMessage("El número de mesa no puede exceder 10 caracteres.");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("La capacidad debe ser mayor a 0.")
            .LessThanOrEqualTo(50).WithMessage("La capacidad no puede exceder 50 personas.");

        RuleFor(x => x.Location)
            .MaximumLength(100).WithMessage("La ubicación no puede exceder 100 caracteres.")
            .When(x => x.Location is not null);
    }
}
