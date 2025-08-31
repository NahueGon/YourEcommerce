using System.ComponentModel.DataAnnotations;

namespace YourEcommerce.DTOs.UserDtos;

public class UserDto
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    public string? Name { get; set; }

    [Display(Name = "Apellido")]
    public string? Lastname { get; set; }

    [Display(Name = "Correo")]
    public string? Email { get; set; }

    [Display(Name = "Telefono")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Direccion")]
    public string? Address { get; set; }
    
    [Display(Name = "Rol")]
    public UserRole? Role { get; set; }

    [Display(Name = "Perfil")]
    public string? ProfileImage { get; set; } = "/img/anonymous-profile.png";
}