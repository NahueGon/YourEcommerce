using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.AuthDtos.LoginDtos;
using YourEcommerceApi.DTOs.AuthDtos.RegisterDtos;
using YourEcommerceApi.Services;

namespace YourEcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Autentication")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService service)
        {
            _authService = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var result = await _authService.Authenticate(loginDto);
            if (result == null) return Unauthorized(new { message = "Credenciales inválidas" });
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterCreateDto registerDto)
        {
            var result = await _authService.Register(registerDto);
            if (result == null) return BadRequest(new { message = "Ya existe un usuario con ese email." });
            return Ok(result);
        }
    }
}
