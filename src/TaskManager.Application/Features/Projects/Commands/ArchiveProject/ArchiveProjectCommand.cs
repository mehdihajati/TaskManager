using MediatR;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Projects.Commands.ArchiveProject;

public record ArchiveProjectCommand(Guid ProjectId) : IRequest<Unit>;
