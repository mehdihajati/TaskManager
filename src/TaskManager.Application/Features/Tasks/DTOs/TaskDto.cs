using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Tasks.DTOs;

public record TaskDto(
    Guid Id,
    Guid ProjectId,
    string Title,
    string Description,
    TaskItemStatus Status,
    DateTimeOffset? DueDate,
    Priority Priority,
    Guid? AssigneeId
);