using MediatR;

namespace TaskManager.Application.Features.Projects.Commands.RemoveMember;

public record RemoveMemberCommand(Guid ProjectId, Guid UserId) : IRequest<Unit>;