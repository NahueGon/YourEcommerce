using System.ComponentModel.DataAnnotations;

namespace YourEcommerce.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "El campo Correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
    public required string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    public required string Password { get; set; }
}

