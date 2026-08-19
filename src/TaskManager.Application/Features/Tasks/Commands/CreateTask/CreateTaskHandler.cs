using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Tasks.Commands.CreateTask;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IProjectRepository _projectRepository;
    private readonly ITaskRepository _taskRepository;

    public CreateTaskHandler(ICurrentUserService currentUser, IProjectRepository projectRepository, ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
        _currentUser = currentUser;
        _projectRepository = projectRepository;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUser.UserId;
        if (currentUser == null)
            throw new ForbiddenException("User is not authenticated.");
        var currentProject = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (currentProject == null)
            throw new NotFoundException("Project not found.");
        var requesterMember = currentProject.Members.FirstOrDefault(x => x.UserId == currentUser.Value);
        if (requesterMember is null)
            throw new ForbiddenException("You are not a member of this project.");
        if (requesterMember.Role != ProjectRole.Owner && requesterMember.Role != ProjectRole.Manager)
            throw new ForbiddenException("You cant create tasks for this project");
        var task = TaskItem.CreateTask(request.Title, request.Description, request.Priority, request.ProjectId, request.DueDate, request.AssigneeId);
        await _taskRepository.AddAsync(task);
        return task.Id;
    }
}