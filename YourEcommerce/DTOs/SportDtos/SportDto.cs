using System.ComponentModel.DataAnnotations;

namespace YourEcommerce.DTOs.SportDtos;

public class SportDto
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    public string? Description { get; set; } = string.Empty;

    [Display(Name = "Portada")]
    public string? SportImage { get; set; } = "/img/anonymous-profile.png";

    public override string ToString() => Name;
}