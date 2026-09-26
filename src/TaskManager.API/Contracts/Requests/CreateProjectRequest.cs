namespace TaskManager.API.Contracts.Requests;

public record CreateProjectRequest(string Name, string? Description, DateTimeOffset Deadline);
