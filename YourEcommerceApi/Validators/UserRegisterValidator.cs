using FluentValidation;
using YourEcommerceApi.DTOs.AuthDtos.RegisterDtos;

namespace YourEcommerceApi.Validators;

public class UserRegisterValidator  : AbstractValidator<UserRegisterCreateDto>
{
    public UserRegisterValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .Length(2, 50).WithMessage("Debe tener entre 2 y 50 caracteres");

        RuleFor(u => u.Lastname)
            .MaximumLength(50).WithMessage("El apellido no puede tener más de 50 caracteres");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("Formato de email inválido");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

        RuleFor(u => u.ConfirmPassword)
            .NotEmpty().WithMessage("La confrimacion de la contraseña es obligatoria")
            .MinimumLength(6).WithMessage("La confrimacion de la contraseña debe tener al menos 6 caracteres")
            .Equal(u => u.Password).WithMessage("Las contraseñas no coinciden");
    }
}
