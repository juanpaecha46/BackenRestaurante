using FluentValidation;
using SeatingService.Application.DTOs;

namespace SeatingService.Application.Validators;

public class AssignOrderValidator : AbstractValidator<AssignOrderRequestDto>
{
    public AssignOrderValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("El Id del pedido es obligatorio.");
    }
}
