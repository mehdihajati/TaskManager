using MediatR;
using TaskManager.Application.Features.Tasks.DTOs;

namespace TaskManager.Application.Features.Tasks.Queries.GetMyTasks;

public record GetMyTasksQuery() : IRequest<IEnumerable<TaskDto>>;