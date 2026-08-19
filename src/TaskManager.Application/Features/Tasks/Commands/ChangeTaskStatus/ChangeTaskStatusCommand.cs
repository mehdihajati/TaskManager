using MediatR;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Tasks.Commands.ChangeTaskStatus;

public record ChangeTaskStatusCommand(Guid TaskId, TaskItemStatus NewStatus) : IRequest<Unit>;
