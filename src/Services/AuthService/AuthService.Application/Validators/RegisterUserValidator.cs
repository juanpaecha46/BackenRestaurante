using AuthService.Application.DTOs;
using FluentValidation;

namespace AuthService.Application.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequestDto>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MaximumLength(100).WithMessage("El nombre no puede superar 100 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido.")
            .EmailAddress().WithMessage("El email no tiene un formato válido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es requerida.")
            .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
            .Matches("[A-Z]").WithMessage("Debe contener al menos una letra mayúscula.")
            .Matches("[0-9]").WithMessage("Debe contener al menos un número.");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("El rol especificado no es válido.");
    }
}
