using MediatR;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Tasks.Commands.ArchiveProject;

public record ArchiveProjectCommand(Guid ProjectId) : IRequest<Unit>;
