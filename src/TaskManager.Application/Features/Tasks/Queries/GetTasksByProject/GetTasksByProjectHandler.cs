using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Features.Tasks.DTOs;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Tasks.Queries.GetTasksByProject;

public class GetTasksByProjectHandler : IRequestHandler<GetTasksByProjectQuery, IEnumerable<TaskDto>>
{
    private readonly ICurrentUserService _currentUser;
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;

    public GetTasksByProjectHandler(ICurrentUserService currentUser, ITaskRepository taskRepository, IProjectRepository projectRepository)
    {
        _currentUser = currentUser;
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<TaskDto>> Handle(GetTasksByProjectQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUser.UserId;
        if (currentUser is null)
            throw new ForbiddenException("You are not authorized!");
        var currentProject = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (currentProject is null)
            throw new NotFoundException("Project not found!");
        var isMember = currentProject.Members.Any(m => m.UserId == currentUser.Value);
        if (!isMember)
            throw new ForbiddenException("You are not a member of this project");
        var memberTasks = await _taskRepository.GetByProjectIdAsync(request.ProjectId);
        var taskList = memberTasks.Select(x => new TaskDto(x.Id, x.ProjectId, x.Title, x.Description, x.Status, x.DueDate, x.Priority, x.AssigneeId));
        return taskList;
    }
}