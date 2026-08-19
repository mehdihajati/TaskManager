using MediatR;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Tasks.Commands.ChangeTaskPriority;

public record ChangeTaskPriorityCommand(Guid TaskId, Priority NewPriority) : IRequest<Unit>;

