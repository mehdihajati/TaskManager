using MediatR;
using TaskManager.Application.Features.Projects.DTOs;

namespace TaskManager.Application.Features.Projects.Queries.GetProjectById;

public record GetProjectByIdQuery(Guid ProjectId) : IRequest<ProjectDTO>;