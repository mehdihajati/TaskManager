using MediatR;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Projects.Commands.ChangeMemberRole;

public record ChangeMemberRoleCommand(Guid UserId, ProjectRole NewMemberRole, Guid ProjectId) : IRequest<Unit>;