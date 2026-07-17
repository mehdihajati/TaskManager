using MediatR;
using TaskManager.Domain.Enums;
namespace TaskManager.Application.Features.Projects.Commands.AddMember;

public record AddMemberCommand(Guid NewMemberId, ProjectRole NewMemberRole, Guid ProjectId) : IRequest<Unit>;