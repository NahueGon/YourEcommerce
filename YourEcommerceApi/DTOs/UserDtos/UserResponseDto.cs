using YourEcommerceApi.Models;

namespace YourEcommerceApi.DTOs.UserDtos;

public class UserResponseDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public UserRole Role { get; set; }
}
