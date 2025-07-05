using API.Auth;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly AdminUserSettings _adminUser;

        public AuthController(AuthService authService, AdminUserSettings adminUser)
        {
            _authService = authService;
            _adminUser = adminUser;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLoginDTO dto)
        {
            if(dto.Email == _adminUser.Email && dto.Senha == _adminUser.Senha)
            {
                var token = _authService.GerarToken(dto.Email);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}