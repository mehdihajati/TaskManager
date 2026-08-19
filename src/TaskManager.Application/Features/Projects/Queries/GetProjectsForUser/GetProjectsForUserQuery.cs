using MediatR;
using TaskManager.Application.Features.Tasks.DTOs;

namespace TaskManager.Application.Features.Tasks.Queries.GetProjectsForUser;

public record GetProjectsForUserQuery() : IRequest<IEnumerable<ProjectDTO>>;