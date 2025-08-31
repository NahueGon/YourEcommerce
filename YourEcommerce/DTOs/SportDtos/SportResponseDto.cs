using System.ComponentModel.DataAnnotations;
using YourEcommerce.DTOs.ProductDtos;

namespace YourEcommerce.DTOs.SportDtos;

public class SportResponseDto
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Portada")]
    public string? SportImage { get; set; } = "/img/anonymous-profile.png";

    public List<ProductDto>? Products { get; set; } = new();
}