using TaskManager.Domain.Enums;

namespace TaskManager.API.Contracts.Requests;

public record AddMemberRequest(Guid NewMemberId, ProjectRole NewMemberRole);

