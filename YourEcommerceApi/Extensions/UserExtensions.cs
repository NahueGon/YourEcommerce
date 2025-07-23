using YourEcommerceApi.DTOs.UserDtos;
using YourEcommerceApi.Models;

namespace YourEcommerceApi.Extensions;

public static class UserExtensions
{
    public static UserResponseDto ToDto(this User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Lastname = user.Lastname,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            Role = user.Role
        };
    }
}