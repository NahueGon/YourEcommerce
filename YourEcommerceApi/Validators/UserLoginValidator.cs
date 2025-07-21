using FluentValidation;
using YourEcommerceApi.DTOs.AuthDtos.LoginDtos;

namespace YourEcommerceApi.Validators;

public class UserLoginValidator  : AbstractValidator<UserLoginDto>
{
    public UserLoginValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("Formato de email inválido");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");
    }
}
