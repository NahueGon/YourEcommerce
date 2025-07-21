namespace YourEcommerceApi.DTOs.UserDtos;

public class UserCreateDto
{
    public required string Name { get; set; }
    public string? Lastname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}
