using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace TaskManagementSystem.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Employee,Manager")]
        [EnableRateLimiting("TaskReadPolicy")]
        public IActionResult GetTasks()
        {
            return Ok("Viewing tasks");
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [EnableRateLimiting("TaskWritePolicy")]
        public IActionResult CreateTask()
        {
            return Ok("Task created");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")]
        [EnableRateLimiting("TaskWritePolicy")]
        public IActionResult UpdateTask(int id)
        {
            return Ok($"Task {id} updated");
        }
    }
}