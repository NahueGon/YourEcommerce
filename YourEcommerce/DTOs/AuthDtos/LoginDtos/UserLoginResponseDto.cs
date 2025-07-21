namespace YourEcommerceApi.DTOs.AuthDtos.LoginDtos;

public class UserLoginResponseDto
{
    public required string Token { get; set; }
    public string? RefreshToken { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
}