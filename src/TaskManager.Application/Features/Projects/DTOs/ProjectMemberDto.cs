using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Tasks.DTOs;

public record ProjectMemberDto(Guid UserId, ProjectRole Role);