using MediatR;

namespace TaskManager.Application.Features.Projects.Commands.CreateProject;

public record CreateProjectCommand(string Name, string? Description, DateTimeOffset Deadline) : IRequest<Guid>;