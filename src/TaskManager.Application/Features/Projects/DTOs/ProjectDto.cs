using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Projects.DTOs;

public record ProjectDTO(Guid Id, Guid OwnerId, string Name, string Description, ProjectStatus Status, DateTimeOffset? Deadline);