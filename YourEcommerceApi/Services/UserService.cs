using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.UserDtos;
using YourEcommerceApi.Services.Interfaces;
using YourEcommerceApi.Models.Users;
using AutoMapper;

namespace YourEcommerceApi.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(AppDbContext appDbContext, IWebHostEnvironment env, IMapper mapper, IPasswordHasher<User> hasher)
    {
        _context = appDbContext;
        _env = env;
        _mapper = mapper;
        _passwordHasher = hasher;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAll()
    {
        var users = await _context.Users
            .ToListAsync();

        return _mapper.Map<List<UserResponseDto>>(users);
    }

    public async Task<UserResponseDto?> Get(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.Id == id);
        if (user == null) return null;

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto?> GetByEmail(string email)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.Email == email);
        if (user == null) return null;

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto> Save(UserCreateDto userDto)
    {
        var user = _mapper.Map<User>(userDto);

        user.Password = _passwordHasher.HashPassword(user, userDto.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto?> Update(int id, UserUpdateDto? userDto)
    {
        if (userDto == null) return null;

        var currentUser = await _context.Users.FindAsync(id);
        if (currentUser == null) return null;

        if (!string.IsNullOrWhiteSpace(userDto.Name) && userDto.Name != currentUser.Name)
            currentUser.Name = userDto.Name;

        if (!string.IsNullOrWhiteSpace(userDto.Lastname) && userDto.Lastname != currentUser.Lastname)
            currentUser.Lastname = userDto.Lastname;

        if (!string.IsNullOrWhiteSpace(userDto.Email) && userDto.Email != currentUser.Email)
            currentUser.Email = userDto.Email;

        if (!string.IsNullOrWhiteSpace(userDto.PhoneNumber) && userDto.PhoneNumber != currentUser.PhoneNumber)
            currentUser.PhoneNumber = userDto.PhoneNumber;

        if (!string.IsNullOrWhiteSpace(userDto.Address) && userDto.Address != currentUser.Address)
            currentUser.Address = userDto.Address;

        if (!string.IsNullOrWhiteSpace(userDto.CurrentPassword) && !string.IsNullOrWhiteSpace(userDto.NewPassword))
        {
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(currentUser, currentUser.Password!, userDto.CurrentPassword);
            
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                return null; 

            currentUser.Password = _passwordHasher.HashPassword(currentUser, userDto.NewPassword);
        }

        if (userDto.ProfileImage != null && userDto.ProfileImage.Length > 0)
        {
            if (!string.IsNullOrEmpty(currentUser.ProfileImage))
            {
                var oldImagePath = Path.Combine(_env.WebRootPath!, currentUser.ProfileImage.Replace("/", Path.DirectorySeparatorChar.ToString()));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            currentUser.ProfileImage = await FileUploadHelper.SaveFileAsync(
                _env,
                userDto.ProfileImage,
                $"img/users/{currentUser.Id}_user",
                $"{currentUser.Id}_profile",
                width: 600,
                height: 600
            );
        }

        await _context.SaveChangesAsync();

        return _mapper.Map<UserResponseDto>(currentUser);
    }

    public async Task<bool> Delete(int id)
    {
        var currentUser = await _context.Users.FindAsync(id);

        if (currentUser == null) return false;

        _context.Remove(currentUser);
        await _context.SaveChangesAsync();

        return true;
    }
}