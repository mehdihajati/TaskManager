using MediatR;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Tasks.Commands.AssignTask;

public record AssignTaskCommand(Guid AssigneeId, ProjectRole RequesterRole) : IRequest<Unit>;
