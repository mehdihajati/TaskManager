using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Projects.Commands.ArchiveProject;

public class ArchiveProjectHandler : IRequestHandler<ArchiveProjectCommand, Unit>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IProjectRepository _projectRepository;

    public ArchiveProjectHandler(ICurrentUserService currentUserService, IProjectRepository projectRepository)
    {
        _currentUserService = currentUserService;
        _projectRepository = projectRepository;
    }
    public async Task<Unit> Handle(ArchiveProjectCommand request, CancellationToken cancellationToken)
    {
        var currentProject = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (currentProject is null)
            throw new NotFoundException("Project not found");
        var currentUser = _currentUserService.UserId;
        if (currentUser is null)
            throw new ForbiddenException("User is not authenticated");
        var requesterMember = currentProject.Members.FirstOrDefault(x => x.UserId == currentUser.Value);
        if (requesterMember is null)
            throw new ForbiddenException("You are not a member of this project");
        var requesterMemberRole = requesterMember.Role;
        currentProject.ArchiveProject(requesterMemberRole);
        await _projectRepository.UpdateAsync(currentProject);
        return Unit.Value;
    }
}