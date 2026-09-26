using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Tasks.DTOs;
using TaskManager.Application.Features.Tasks.Queries.GetMyTasks;
using TaskManager.Application.Features.Tasks.Queries.GetTaskById;
using TaskManager.Application.Features.Tasks.Queries.GetTasksByProject;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyTasks()
        {
            var query = new GetMyTasksQuery();
            IEnumerable<TaskDto> queryResult = await _mediator.Send(query);
            return Ok(queryResult);
        }
        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskById(Guid taskId)
        {
            var query = new GetTaskByIdQuery(taskId);
            TaskDto queryResult = await _mediator.Send(query);
            return Ok(queryResult);

        }
        [HttpGet("{projectId}/tasks")]
        public async Task<IActionResult> GetTasksByProject(Guid projectId)
        {
            var query = new GetTasksByProjectQuery(projectId);
            IEnumerable<TaskDto> queryResult = await _mediator.Send(query);
            return Ok(queryResult);
        }

    }
}
