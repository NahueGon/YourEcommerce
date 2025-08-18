namespace YourEcommerceApi.Models.Users;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Lastname { get; set; } = string.Empty;
    public required string Email { get; set; }
    public string? Password { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public string? ProfileImage { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Customer;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
