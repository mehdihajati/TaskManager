using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities.Aggregates;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Projects.Commands.RemoveMember;

public class RemoveMemberHandler : IRequestHandler<RemoveMemberCommand, Unit>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IProjectRepository _projectRepository;

    public RemoveMemberHandler(ICurrentUserService currentUser, IProjectRepository projectRepository)
    {
        _currentUser = currentUser;
        _projectRepository = projectRepository;
    }

    public async Task<Unit> Handle(RemoveMemberCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUser.UserId;
        if (currentUser is null)
            throw new ForbiddenException("User is not authenticated.");

        var project = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (project is null)
            throw new NotFoundException("Project not found.");
        var targetMember = project.Members.FirstOrDefault(x => x.UserId == request.UserId);

        if (targetMember is null)
            throw new NotFoundException("This user is not a member of the project.");

        var requesterMember = project.Members.FirstOrDefault(x => x.UserId == currentUser.Value);
        if (requesterMember is null)
            throw new ForbiddenException("You are not a member of this project.");
        targetMember.Remove(requesterMember.Role);
        await _projectRepository.UpdateAsync(project);
        return Unit.Value;
    }
}