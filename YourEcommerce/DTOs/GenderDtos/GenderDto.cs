using System.ComponentModel.DataAnnotations;

namespace YourEcommerce.DTOs.GenderDtos;

public class GenderDto
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    public string? Description { get; set; } = string.Empty;

    [Display(Name = "Portada")]
    public string? GenderImage { get; set; } = "/img/anonymous-profile.png";

    public override string ToString() => Name;
}