using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Projects.Commands.AddMember;

public class AddMemberHandler : IRequestHandler<AddMemberCommand, Unit>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentUserService _currentUser;

    public AddMemberHandler(IProjectRepository projectRepository, ICurrentUserService currentUser)
    {
        _projectRepository = projectRepository;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(AddMemberCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.UserId;
        if (currentUserId is null)
            throw new ForbiddenException("User is not authenticated.");
        var projectExist = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (projectExist is null)
            throw new NotFoundException("Project not found.");
        var membershipStatus = projectExist.Members.FirstOrDefault(m => m.UserId == currentUserId.Value);
        if (membershipStatus is null)
            throw new ForbiddenException("not a member of this project at all");
        projectExist.AddMember(request.NewMemberId, request.NewMemberRole, membershipStatus.Role);

        await _projectRepository.UpdateAsync(projectExist);
        return Unit.Value;
    }
}