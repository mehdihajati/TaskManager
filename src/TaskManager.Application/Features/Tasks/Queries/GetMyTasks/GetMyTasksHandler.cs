using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Features.Tasks.DTOs;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Tasks.Queries.GetMyTasks;

public class GetMyTasksHandler : IRequestHandler<GetMyTasksQuery, IEnumerable<TaskDto>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICurrentUserService _userService;

    public GetMyTasksHandler(ITaskRepository taskRepository, ICurrentUserService userService)
    {
        _taskRepository = taskRepository;
        _userService = userService;
    }

    public async Task<IEnumerable<TaskDto>> Handle(GetMyTasksQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _userService.UserId;
        if (currentUser is null)
            throw new ForbiddenException("User is not authenticated.");
        var tasks = await _taskRepository.GetByAssigneeIdAsync(currentUser.Value);
        var taskList = tasks.Select(x => new TaskDto(x.Id, x.ProjectId, x.Title, x.Description, x.Status, x.DueDate, x.Priority, x.AssigneeId));
        return taskList;
    }
}