using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.UserDtos;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Tags("Users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var users = await _userService.GetAll();
            if (users == null || !users.Any()) return NotFound("No se encontraron usuarios.");

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUser(int id)
        {
            var user = await _userService.Get(id);
            if (user == null) return NotFound("Usuario no encontrado");

            return Ok(user);
        }

        [HttpGet("email")]
        public async Task<ActionResult<UserResponseDto>> GetUserByEmail([FromQuery] string email)
        {
            var user = await _userService.GetByEmail(email);
            if (user == null) return NotFound("Usuario no encontrado");

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] UserCreateDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var responseDto = await _userService.Save(userDto);

            return CreatedAtAction(nameof(GetUser), new { id = responseDto.Id }, responseDto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _userService.Update(id, userDto);
            if (updated == null) return NotFound("Usuario no encontrado o datos inválidos");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubcategory(int id)
        {
            var deleted = await _userService.Delete(id);
            if (!deleted) return NotFound("Usuario no encontrado");

            return Ok(new { message = "Usuario eliminado correctamente" });
        }
    }
}