using MediatR;

namespace TaskManager.Application.Features.Tasks.Commands.RemoveMember;

public record RemoveMemberCommand(Guid ProjectId, Guid UserId) : IRequest<Unit>;