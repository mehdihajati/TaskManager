using MediatR;
using TaskManager.Application.Features.Projects.DTOs;

namespace TaskManager.Application.Features.Projects.Queries.GetProjectsForUser;

public record GetProjectsForUserQuery() : IRequest<IEnumerable<ProjectDTO>>;