using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Contracts.Requests;
using TaskManager.Application.Features.Tasks.Commands.AddMember;
using TaskManager.Application.Features.Tasks.Commands.ArchiveProject;
using TaskManager.Application.Features.Tasks.Commands.ChangeMemberRole;
using TaskManager.Application.Features.Tasks.Commands.ChangeProjectStatus;
using TaskManager.Application.Features.Tasks.Commands.CreateProject;
using TaskManager.Application.Features.Tasks.Commands.RemoveMember;
using TaskManager.Application.Features.Tasks.DTOs;
using TaskManager.Application.Features.Tasks.Queries.GetProjectById;
using TaskManager.Application.Features.Tasks.Queries.GetProjectMembers;
using TaskManager.Application.Features.Tasks.Queries.GetProjectsForUser;

namespace TaskManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetProjectById(Guid projectId)
    {
        var query = new GetProjectByIdQuery(projectId);
        ProjectDTO queryResult = await _mediator.Send(query);
        return Ok(queryResult);

    }
    [HttpGet]
    public async Task<IActionResult> GetProjectsForUser()
    {
        var query = new GetProjectsForUserQuery();
        IEnumerable<ProjectDTO> queryResult = await _mediator.Send(query);
        return Ok(queryResult);
    }
    [HttpGet("{projectId}/members")]
    public async Task<IActionResult> GetProjectMembers(Guid projectId)
    {
        var query = new GetProjectMembersQuery(projectId);
        IEnumerable<ProjectMemberDto> queryResult = await _mediator.Send(query);
        return Ok(queryResult);
    }
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
    {
        var command = new CreateProjectCommand(request.Name, request.Description, request.Deadline);
        var projectId = await _mediator.Send(command);
        return Created($"api/projects/{projectId}", new { Id = projectId });
    }
    [HttpPost("{projectId}/members")]
    public async Task<IActionResult> AddMember(Guid projectId, [FromBody] AddMemberRequest request)
    {
        var command = new AddMemberCommand(request.NewMemberId, request.NewMemberRole, projectId);
        var result = await _mediator.Send(command);
        return NoContent();
    }
    [HttpPut("{projectId}/members/{userId}/role")]
    public async Task<IActionResult> ChangeMemberRole(Guid projectId, Guid userId, [FromBody] ChangeMemberRoleRequest request)
    {
        var command = new ChangeMemberRoleCommand(userId, request.NewMemberRole, projectId);
        await _mediator.Send(command);
        return NoContent();
    }
    [HttpPut("{projectId}/status")]
    public async Task<IActionResult> ChangeProjectStatus(Guid projectId, [FromBody] ChangeProjectStatusRequest request)
    {
        var command = new ChangeProjectStatusCommand(projectId, request.NewStatus);
        await _mediator.Send(command);
        return NoContent();
    }
    [HttpDelete("{projectId}/members/{userId}")]
    public async Task<IActionResult> RemoveMwmber(Guid projectId, Guid userId)
    {
        var command = new RemoveMemberCommand(projectId, userId);
        var result = await _mediator.Send(command);
        return NoContent();
    }
    [HttpDelete("{projectId}")]
    public async Task<IActionResult> ArchiveProject(Guid projectId)
    {
        var command = new ArchiveProjectCommand(projectId);
        await _mediator.Send(command);
        return NoContent();
    }

}
