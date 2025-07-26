using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.UserDtos;
using YourEcommerceApi.Services.Interfaces;
using YourEcommerceApi.Extensions;
using YourEcommerceApi.Models.Users;

namespace YourEcommerceApi.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(AppDbContext appDbContext, IPasswordHasher<User> hasher)
    {
        _context = appDbContext;
        _passwordHasher = hasher;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAll()
    {
        var users = await _context.Users.ToListAsync();

        return users.Select(user => user.ToDto());
    }

    public async Task<UserResponseDto?> Get(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user == null) return null;

        return user.ToDto();
    }

    public async Task<UserResponseDto?> GetByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        if (user == null) return null;

        return user.ToDto();
    }

    public async Task<UserResponseDto> Save(UserCreateDto userDto)
    {
        var user = new User
        {
            Name = userDto.Name,
            Lastname = userDto.Lastname,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            Address = userDto.Address
        };

        user.Password = _passwordHasher.HashPassword(user, userDto.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user.ToDto();
    }

    public async Task<UserResponseDto?> Update(int id, UserUpdateDto? userDto)
    {
        if (userDto == null) return null;

        var currentUser = await _context.Users.FindAsync(id);
        if (currentUser == null) return null;

        if (!string.IsNullOrWhiteSpace(userDto.Name))
            currentUser.Name = userDto.Name;

        if (!string.IsNullOrWhiteSpace(userDto.Lastname))
            currentUser.Lastname = userDto.Lastname;

        if (!string.IsNullOrWhiteSpace(userDto.Email))
            currentUser.Email = userDto.Email;

        if (!string.IsNullOrWhiteSpace(userDto.Password))
            currentUser.Password = _passwordHasher.HashPassword(currentUser, userDto.Password);

        if (!string.IsNullOrWhiteSpace(userDto.PhoneNumber))
            currentUser.PhoneNumber = userDto.PhoneNumber;

        if (!string.IsNullOrWhiteSpace(userDto.Address))
            currentUser.Address = userDto.Address;

        await _context.SaveChangesAsync();

        return currentUser.ToDto();
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