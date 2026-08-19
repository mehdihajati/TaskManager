using MediatR;

namespace TaskManager.Application.Features.Tasks.Commands.CloseTask;

public record CloseTaskCommand(Guid TaskId) : IRequest<Unit>;
