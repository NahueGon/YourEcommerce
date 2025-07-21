using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.AuthDtos.LoginDtos;
using YourEcommerceApi.DTOs.AuthDtos.RegisterDtos;
using YourEcommerceApi.Models;

namespace YourEcommerceApi.Services;

public class AuthService : IAuthService
{
    AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IPasswordHasher<User> hasher, IConfiguration configuration)
    {
        _context = context;
        _passwordHasher = hasher;
        _configuration = configuration;
    }

    public async Task<UserLoginResponseDto?> Authenticate(UserLoginDto loginDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null) return null;

        if (string.IsNullOrEmpty(user.Password)) return null;

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
        if (result == PasswordVerificationResult.Failed) return null;

        return GenerateJwtToken(user);
    }

    private UserLoginResponseDto GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var keyString = _configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(keyString)) throw new InvalidOperationException("JWT key is not configured.");

        var keyBytes = Convert.FromBase64String(keyString);
        var key = new SymmetricSecurityKey(keyBytes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new UserLoginResponseDto
        {
            Token = tokenString,
            Email = user.Email,
            UserName = user.Name + ' ' + user.Lastname
        };
    }
    
    public async Task<UserRegisterResponseDto?> Register(UserRegisterCreateDto registerDto)
    {
        var existingUser = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == registerDto.Email);


        if (existingUser != null)
            return null;
    
        var user = new User
        {
            Name = registerDto.Name,
            Lastname = registerDto.Lastname,
            Email = registerDto.Email
        };

        user.Password = _passwordHasher.HashPassword(user, registerDto.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserRegisterResponseDto
        {
            Name = user.Name,
            Lastname = user.Lastname,
            Email = user.Email
        };
        
    }
}
