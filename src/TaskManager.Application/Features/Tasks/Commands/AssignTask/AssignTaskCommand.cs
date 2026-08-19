using MediatR;

namespace TaskManager.Application.Features.Tasks.Commands.AssignTask;

public record AssignTaskCommand(Guid AssigneeId, Guid TaskId) : IRequest<Unit>;
