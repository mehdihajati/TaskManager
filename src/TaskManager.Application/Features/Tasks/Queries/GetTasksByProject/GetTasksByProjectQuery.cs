using MediatR;
using TaskManager.Application.Features.Tasks.DTOs;

namespace TaskManager.Application.Features.Tasks.Queries.GetTasksByProject;

public record GetTasksByProjectQuery(Guid ProjectId) : IRequest<IEnumerable<TaskDto>>;
