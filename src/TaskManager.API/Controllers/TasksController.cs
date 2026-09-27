using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Contracts.Requests;
using TaskManager.Application.Features.Tasks.Commands.AssignTask;
using TaskManager.Application.Features.Tasks.Commands.ChangeTaskPriority;
using TaskManager.Application.Features.Tasks.Commands.ChangeTaskStatus;
using TaskManager.Application.Features.Tasks.Commands.CloseTask;
using TaskManager.Application.Features.Tasks.Commands.CreateTask;
using TaskManager.Application.Features.Tasks.Commands.UnassignTask;
using TaskManager.Application.Features.Tasks.DTOs;
using TaskManager.Application.Features.Tasks.Queries.GetMyTasks;
using TaskManager.Application.Features.Tasks.Queries.GetTaskById;
using TaskManager.Application.Features.Tasks.Queries.GetTasksByProject;

namespace TaskManager.API.Controllers;

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
    [HttpPost("{taskId}/assign/{assigneeId}")]
    public async Task<IActionResult> AssignTask(Guid taskId, Guid assigneeId)
    {
        var command = new AssignTaskCommand(assigneeId, taskId);
        await _mediator.Send(command);
        return NoContent();
    }
    [HttpDelete("{taskId}/assignee")]
    public async Task<IActionResult> UnAssignTask(Guid taskId)
    {
        var command = new UnassignTaskCommand(taskId);
        await _mediator.Send(command);
        return NoContent();
    }
    [HttpPatch("{taskId}/status")]
    public async Task<IActionResult> ChangeTaskStatus(Guid taskId, [FromBody] ChangeTaskStatusRequest request)
    {
        var command = new ChangeTaskStatusCommand(taskId, request.NewStatus);
        await _mediator.Send(command);
        return NoContent();
    }
    [HttpPatch("{taskId}/priority")]
    public async Task<IActionResult> ChangeTaskPriority(Guid taskId, [FromBody] ChangeTaskPriorityRequest request)
    {
        var command = new ChangeTaskPriorityCommand(taskId, request.NewPriority);
        await _mediator.Send(command);
        return NoContent();
    }
    [HttpPatch("{taskId}/close")]
    public async Task<IActionResult> CloseTask(Guid taskId)
    {
        var command = new CloseTaskCommand(taskId);
        await _mediator.Send(command);
        return NoContent();
    }
}
