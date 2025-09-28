using Microsoft.AspNetCore.Mvc;
using ToDoList_API.Models;
using ToDoList_API.Services;

namespace ToDoList_API.Controllers
{
    [ApiController]
    [Route("to-do-list")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("task")]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateModel task)
        {
            return Ok(await _taskService.CreateTask(task));
        }

        [HttpGet("tasks")]
        public async Task<IActionResult> GetTasks()
        {
            return Ok(await _taskService.GetTasks());
        }

        [HttpDelete("task/{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            try
            {
                await _taskService.DeleteTask(id);

                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("task/{id}")]
        public async Task<IActionResult> EditTask([FromRoute] Guid id,
            [FromBody] TaskEditModel task)
        {
            try
            {
                await _taskService.EditTask(id, task);

                return Ok();
            }
            catch (Exception e)
            {
                if (e.Message == "Not found")
                {
                    return NotFound();
                }

                return BadRequest();
            }
        }

        [HttpPatch("check/task/{id}")]
        public async Task<IActionResult> CheckTask([FromRoute] Guid id)
        {
            try
            {
                await _taskService.CheckTask(id);

                return Ok();
            }
            catch (Exception e)
            {
                if (e.Message == "Not found")
                {
                    return NotFound();
                }

                return BadRequest();
            }
        }

        [HttpDelete("check/task/{id}")]
        public async Task<IActionResult> UncheckTask([FromRoute] Guid id)
        {
            try
            {
                await _taskService.UncheckTask(id);

                return Ok();
            }
            catch (Exception e)
            {
                if (e.Message == "Not found")
                {
                    return NotFound();
                }

                return BadRequest();
            }
        }

        [HttpPost("tasks/upload")]
        public async Task<IActionResult> UploadTasks([FromBody] List<TaskModel> tasks)
        {
            await _taskService.UploadTasks(tasks);

            return Ok();
        }
    }
}
