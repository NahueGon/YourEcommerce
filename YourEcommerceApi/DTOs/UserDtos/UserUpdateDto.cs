
namespace YourEcommerceApi.DTOs.UserDtos;

public class UserUpdateDto
{
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }    
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public IFormFile? ProfileImage { get; set; }
}
