using System.Security.Claims;
using AutoMapper;
using YourEcommerce.DTOs.UserDtos;
using YourEcommerce.Repositories.Interfaces;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _userRepository = repository;
        _mapper = mapper;
    }

    public async Task<UserResponseDto?> Get(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is null) return null;

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto?> GetByEmail(string email)
    {
        var user = await _userRepository.GetByEmail(email);
        if (user == null) return null;
        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto?> GetCurrent(ClaimsPrincipal claimsPrincipal)
    {
        var userEntity = await _userRepository.GetCurrent(claimsPrincipal);
        if (userEntity == null) return null;

        return _mapper.Map<UserResponseDto>(userEntity);
    }

    public async Task<List<UserDto>> GetAllForTable()
    {
        var users = await _userRepository.GetAll();
        return users.Select(u => _mapper.Map<UserDto>(u)).ToList();
    }

    public async Task<UserUpdateDto?> GetForEdit(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is null) return null;

        return _mapper.Map<UserUpdateDto>(user);
    }

    public async Task<UserDto?> Create(UserCreateDto userDto)
    {
        var created = await _userRepository.Create(userDto);
        if (created == null) return null;
        return _mapper.Map<UserDto>(created);
    }

    public async Task<UserDto?> Update(int id, UserUpdateDto userDto)
    {
        var updated = await _userRepository.Update(id, userDto);
        if (updated == null) return null;
        return _mapper.Map<UserDto>(updated);
    }

    public Task<bool> Delete(int id)
    {
        return _userRepository.Delete(id);
    }
}