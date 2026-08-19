using MediatR;
namespace TaskManager.Application.Features.Tasks.Commands.UnassignTask;

public record UnassignTaskCommand(Guid TaskId) : IRequest<Unit>;
