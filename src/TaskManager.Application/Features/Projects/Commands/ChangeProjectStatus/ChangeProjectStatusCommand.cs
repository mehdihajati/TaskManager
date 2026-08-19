using MediatR;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Tasks.Commands.ChangeProjectStatus;

public record ChangeProjectStatusCommand(Guid ProjectId, ProjectStatus NewStatus) : IRequest<Unit>;