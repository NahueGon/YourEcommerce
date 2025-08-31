using System.ComponentModel.DataAnnotations;
using YourEcommerce.Attributes;

namespace YourEcommerce.DTOs.UserDtos;

public class UserCreateDto
{
    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "El nombre no puede superar los 500 caracteres.")]
    public string? Lastname { get; set; } = string.Empty;

    [AllowedExtensions(new string[] { ".webp", ".jpg", ".jpeg", ".png", ".gif" })]
    public IFormFile? ProfileImage { get; set; }
}