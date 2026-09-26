using TaskManager.Domain.Enums;

namespace TaskManager.API.Contracts.Requests
{
    public record CreateTaskRequest(string Title, string? Description, Priority Priority, DateTimeOffset? DueDate, Guid? AssigneeId);
}
