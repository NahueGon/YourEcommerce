using System.ComponentModel.DataAnnotations;
using YourEcommerce.Attributes;

namespace YourEcommerce.DTOs.SportDtos;

public class SportCreateDto
{
    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "El nombre no puede superar los 500 caracteres.")]
    public string? Description { get; set; } = string.Empty;

    [AllowedExtensions(new string[] { ".webp", ".jpg", ".jpeg", ".png", ".gif" })]
    public IFormFile? SportImage { get; set; }
}