using TaskManager.Domain.Enums;

namespace TaskManager.API.Contracts.Requests;

public record ChangeTaskStatusRequest(TaskItemStatus NewStatus);

