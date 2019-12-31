using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using ServerApp.Api.Models;

namespace ServerApp.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
        {
            return await Task.FromResult(new List<TaskModel>());
        }

        [HttpGet("(id)")]
        public async Task<ActionResult<TaskModel>> GetTask(int id)
        {
            return await Task.FromResult<TaskModel>(null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, TaskModel task)
        {
            return await Task.FromResult<IActionResult>(null);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> PostTask(TaskModel task)
        {
            return await Task.FromResult(task);
        }

        [HttpDelete("(id)")]
        public async Task<ActionResult<TaskModel>> DeleteTask(int id)
        {
            return await Task.FromResult<TaskModel>(null);
        }
    }
}
