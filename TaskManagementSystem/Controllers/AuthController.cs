using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TaskManagementSystem.Models;
using TaskManagementSystem.Utils;

namespace TaskManagementSystem.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        private readonly List<(string Username, string Password, string Role)> users = new()
        {
            ("admin", "admin123", "Admin"),
            ("manager", "manager123", "Manager"),
            ("employee", "employee123", "Employee")
        };

        [HttpPost("login")]
        [APIKeyAuthorize] 
        [EnableRateLimiting("LoginPolicy")] 
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = users.FirstOrDefault(u =>
                u.Username == model.Username && u.Password == model.Password);

            if (user == default)
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user.Username, user.Role);

            return Ok(new
            {
                token,
                username = user.Username,
                role = user.Role
            });
        }
    }
}