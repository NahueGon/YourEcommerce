using System.ComponentModel.DataAnnotations;

namespace YourEcommerce.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    public required string Name { get; set; }
    public string? Lastname { get; set; }
    
    [Required(ErrorMessage = "El campo Correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
    public required string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "El campo Confirmar Contraseña es obligatorio.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
    public required string ConfirmPassword { get; set; }
}
