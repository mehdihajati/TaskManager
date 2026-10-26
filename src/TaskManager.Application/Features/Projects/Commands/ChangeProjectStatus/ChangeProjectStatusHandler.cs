using MediatR;
using TaskManager.Application.Common;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Tasks.Commands.ChangeProjectStatus;

public class ChangeProjectStatusHandler : IRequestHandler<ChangeProjectStatusCommand, Unit>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IProjectRepository _projectRepository;
    private readonly ICacheService _cache;

    public ChangeProjectStatusHandler(ICurrentUserService currentUserService, IProjectRepository projectRepository, ICacheService cache)
    {
        _currentUserService = currentUserService;
        _projectRepository = projectRepository;
        _cache = cache;
    }

    public async Task<Unit> Handle(ChangeProjectStatusCommand request, CancellationToken cancellationToken)
    {
        var currentProject = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (currentProject is null)
            throw new NotFoundException("Project not found");
        var currentUser = _currentUserService.UserId;
        if (currentUser is null)
            throw new ForbiddenException("User is not authenticated");
        var requesterMember = currentProject.Members.FirstOrDefault(x => x.UserId == currentUser.Value && !x.IsDeleted);
        if (requesterMember is null)
            throw new ForbiddenException("You are not a member of this project");
        var requesterMemberRole = requesterMember.Role;
        currentProject.ChangeStatus(requesterMemberRole, request.NewStatus);
        await _projectRepository.UpdateAsync(currentProject);
        await _cache.RemoveAsync(CacheKeys.Project(request.ProjectId), cancellationToken);
        return Unit.Value;
    }
}