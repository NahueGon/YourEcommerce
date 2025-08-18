using System.ComponentModel.DataAnnotations;

namespace YourEcommerce.DTOs.UserDtos;

public class UserUpdateViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
    public string? Name { get; set; }

    [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
    public string? Lastname { get; set; }

    [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
    public string? Email { get; set; }

    [Phone(ErrorMessage = "El número de teléfono no es válido.")]
    [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres.")]
    public string? PhoneNumber { get; set; }

    [StringLength(100, ErrorMessage = "La dirección no puede superar los 100 caracteres.")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "El campo Contraseña Actual es obligatorio.")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "La actual contraseña debe tener al menos 6 caracteres.")]
    public string? CurrentPassword { get; set; }

    [Required(ErrorMessage = "El campo Nueva Contraseña es obligatorio.")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "La nueva contraseña debe tener al menos 6 caracteres.")]
    public string? NewPassword { get; set; }

    public IFormFile? ProfileImage { get; set; }
    public string? ProfileImageUrl { get; set; }
}