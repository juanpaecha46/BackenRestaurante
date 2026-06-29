using FluentValidation;
using OrderService.Application.DTOs;

namespace OrderService.Application.Validators;

public class UpdateItemQuantityValidator : AbstractValidator<UpdateItemQuantityRequestDto>
{
    public UpdateItemQuantityValidator()
    {
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
