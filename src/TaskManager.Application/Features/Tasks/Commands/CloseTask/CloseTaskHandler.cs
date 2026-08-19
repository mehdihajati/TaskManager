using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Tasks.Commands.CloseTask;

public class CloseTaskHandler : IRequestHandler<CloseTaskCommand, Unit>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentUserService _currentUser;
    public CloseTaskHandler(ITaskRepository taskRepository, IProjectRepository projectRepository, ICurrentUserService currentUser)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
        _currentUser = currentUser;
    }
    public async Task<Unit> Handle(CloseTaskCommand request, CancellationToken cancellationToken)
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
        var requesterMembership = selectedProject.Members.FirstOrDefault(m => m.UserId == currentUser.Value);
        if (requesterMembership is null)
            throw new ForbiddenException("You are not a member of this project");
        task.Close(requesterMembership.Role);
        await _taskRepository.UpdateAsync(task);
        return Unit.Value;
    }
}