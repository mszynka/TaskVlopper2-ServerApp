using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Api.Models;
using ServerApp.Api.Records;
using StructureMap;

namespace ServerApp.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository _taskRepository)
        {
            this._taskRepository = _taskRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskModel>> GetTasks()
        {
            return (await _taskRepository.QueryAsync())
                .Select(TaskModel.FromRecord);
        }

        [HttpGet("(id)")]
        public async Task<TaskModel> GetTask(int id)
        {
            return TaskModel.FromRecord(await _taskRepository.GetAsync(id));
        }

        [HttpPut("{id}")]
        public async Task PutTask(int id, TaskModel task)
        {
            await _taskRepository.CreateAsync(task.ToRecord());
        }

        [HttpPost]
        public async Task<TaskModel> PostTask(TaskModel task)
        {
            return TaskModel.FromRecord(await _taskRepository.UpdateAsync(task.ToRecord()));
        }

        [HttpDelete("(id)")]
        public async Task<TaskModel> DeleteTask(int id)
        {
            return TaskModel.FromRecord(await _taskRepository.DeleteAsync(id));
        }
    }
}