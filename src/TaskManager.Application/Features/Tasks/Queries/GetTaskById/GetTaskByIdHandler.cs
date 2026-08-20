using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Features.Tasks.DTOs;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Tasks.Queries.GetTaskById;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TaskDto>
{
    private readonly ICurrentUserService _currentUser;
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;

    public GetTaskByIdHandler(ICurrentUserService currentUser, ITaskRepository taskRepository, IProjectRepository projectRepository)
    {
        _currentUser = currentUser;
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
    }

    public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUser.UserId;
        if (currentUser == null)
            throw new ForbiddenException("User Is not Authorized");
        var task = await _taskRepository.GetByIdAsync(request.TaskId);
        if (task is null)
            throw new NotFoundException("Task Doesnt Exist.");
        var currentProject = await _projectRepository.GetByIdAsync(task.ProjectId);
        if (currentProject is null)
            throw new NotFoundException("ProjectNotFount");
        var isMember = currentProject.Members.Any(x => x.UserId == currentUser.Value);
        if (!isMember)
            throw new ForbiddenException("You are not a part of this project");
        return new TaskDto(task.Id, task.ProjectId, task.Title, task.Description, task.Status, task.DueDate, task.Priority, task.AssigneeId);
    }
}