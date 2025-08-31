using System.ComponentModel.DataAnnotations;
using YourEcommerce.Attributes;

namespace YourEcommerce.DTOs.BrandDtos;

public class BrandUpdateDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "El nombre no puede superar los 500 caracteres.")]
    public string? Description { get; set; } = string.Empty;

    [AllowedExtensions(new string[] { ".webp", ".jpg", ".jpeg", ".png", ".gif" })]
    public IFormFile? BrandImage { get; set; }
    public string? BrandImageUrl { get; set; } = "/img/anonymous-profile.png";
}