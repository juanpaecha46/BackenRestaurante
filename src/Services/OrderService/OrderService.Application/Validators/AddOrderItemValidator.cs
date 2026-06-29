using FluentValidation;
using OrderService.Application.DTOs;

namespace OrderService.Application.Validators;

public class AddOrderItemValidator : AbstractValidator<AddOrderItemRequestDto>
{
    public AddOrderItemValidator()
    {
        RuleFor(x => x.MenuItemId).NotEmpty();
        RuleFor(x => x.MenuItemName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.UnitPrice).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
