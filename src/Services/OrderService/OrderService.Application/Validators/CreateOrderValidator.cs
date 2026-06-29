using FluentValidation;
using OrderService.Application.DTOs;

namespace OrderService.Application.Validators;

public class CreateOrderValidator : AbstractValidator<CreateOrderRequestDto>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.TableId).NotEmpty();
        RuleFor(x => x.WaiterId).NotEmpty();
        RuleFor(x => x.Items).NotEmpty().WithMessage("El pedido debe tener al menos un item.");
        RuleForEach(x => x.Items).SetValidator(new CreateOrderItemValidator());
    }
}

public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemDto>
{
    public CreateOrderItemValidator()
    {
        RuleFor(x => x.MenuItemId).NotEmpty();
        RuleFor(x => x.MenuItemName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.UnitPrice).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
