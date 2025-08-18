using System.ComponentModel.DataAnnotations;

namespace YourEcommerce.DTOs.BrandDtos;

public class BrandDto
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Portada")]
    public string? BrandImage { get; set; } = "/img/anonymous-profile.png";
}