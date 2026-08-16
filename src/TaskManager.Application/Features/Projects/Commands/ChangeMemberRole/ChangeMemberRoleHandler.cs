using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Projects.Commands.ChangeMemberRole;

public class ChangeMemberRoleHandler : IRequestHandler<ChangeMemberRoleCommand, Unit>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IProjectRepository _projectRepository;

    public ChangeMemberRoleHandler(ICurrentUserService currentUser, IProjectRepository projectRepository)
    {
        _currentUser = currentUser;
        _projectRepository = projectRepository;
    }

    public async Task<Unit> Handle(ChangeMemberRoleCommand request, CancellationToken cancellationToken)
    {
        var currentProject = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (currentProject is null)
            throw new NotFoundException("Project not found");
        var currentUserId = _currentUser.UserId;
        if (currentUserId is null)
            throw new ForbiddenException("This User does not exist in this project");
        var targetMember = currentProject.Members.FirstOrDefault(x => x.UserId == request.UserId);
        if (targetMember is null)
            throw new NotFoundException("this user isnt a member of this project");
        var requesterMember = currentProject.Members.FirstOrDefault(x => x.UserId == currentUserId.Value);
        if (requesterMember is null)
            throw new ForbiddenException("You are not a member of this project");
        var requesterMemberRole = requesterMember.Role;
        targetMember.ChangeRole(request.NewMemberRole, requesterMemberRole);
        await _projectRepository.UpdateAsync(currentProject);
        return Unit.Value;

    }
}