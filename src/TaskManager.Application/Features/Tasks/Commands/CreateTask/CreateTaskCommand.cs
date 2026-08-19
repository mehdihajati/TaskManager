using MediatR;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Tasks.Commands.CreateTask;

public record CreateTaskCommand(string Title, string? Description, Priority Priority, Guid ProjectId, DateTimeOffset? DueDate, Guid? AssigneeId) : IRequest<Guid>;
