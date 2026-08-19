using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Tasks.Commands.ChangeTaskStatus;

public class ChangeTaskStatusHandler : IRequestHandler<ChangeTaskStatusCommand, Unit>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IProjectRepository _projectRepository;
    private readonly ITaskRepository _taskRepository;

    public ChangeTaskStatusHandler(ICurrentUserService currentUser, IProjectRepository projectRepository, ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
        _currentUser = currentUser;
        _projectRepository = projectRepository;
    }
    public async Task<Unit> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUser.UserId;
        if (currentUser is null)
            throw new ForbiddenException("User is not Authorized");
        var task = await _taskRepository.GetByIdAsync(request.TaskId);
        if (task is null)
            throw new NotFoundException("Selected Task doesnt exist ");
        var selectedProject = await _projectRepository.GetByIdAsync(task.ProjectId);
        if (selectedProject is null)
            throw new NotFoundException("Project not found!");
        var requesterMember = selectedProject.Members.FirstOrDefault(x => x.UserId == currentUser.Value);
        if (requesterMember is null)
            throw new ForbiddenException("You are not Authorized");
        task.ChangeStatus(requesterMember.Role, request.NewStatus, currentUser.Value);
        await _taskRepository.UpdateAsync(task);
        return Unit.Value;
    }
}
