namespace YourEcommerceApi.DTOs.AuthDtos.RegisterDtos;

public class UserRegisterResponseDto
{
    public required string Name { get; set; }
    public string? Lastname { get; set; }
    public required string Email { get; set; }
}
