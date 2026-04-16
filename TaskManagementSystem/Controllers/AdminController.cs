using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [EnableRateLimiting("AdminPolicy")] 
    public class AdminController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Username = "admin" },
            new User { Id = 2, Username = "manager" },
            new User { Id = 3, Username = "employee" }
        };

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return Ok(users);
        }

        [HttpDelete("users/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound("User not found");

            users.Remove(user);

            return Ok(new
            {
                message = $"User {id} deleted successfully",
                remainingUsers = users
            });
        }
    }
}