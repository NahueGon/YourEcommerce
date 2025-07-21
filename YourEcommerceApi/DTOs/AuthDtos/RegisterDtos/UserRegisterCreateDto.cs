namespace YourEcommerceApi.DTOs.AuthDtos.RegisterDtos;

public class UserRegisterCreateDto
{
    public required string Name { get; set; }
    public string? Lastname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
}
