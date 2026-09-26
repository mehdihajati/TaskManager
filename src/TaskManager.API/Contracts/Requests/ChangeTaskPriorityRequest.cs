using TaskManager.Domain.Enums;

namespace TaskManager.API.Contracts.Requests;

public record ChangeTaskPriorityRequest(Priority NewPriority);

