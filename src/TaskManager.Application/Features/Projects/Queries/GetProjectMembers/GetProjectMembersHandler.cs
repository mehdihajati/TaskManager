using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Features.Tasks.DTOs;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Tasks.Queries.GetProjectMembers;

public class GetProjectMembersHandler : IRequestHandler<GetProjectMembersQuery, IEnumerable<ProjectMemberDto>>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IProjectRepository _projectRepository;

    public GetProjectMembersHandler(ICurrentUserService currentUser, IProjectRepository projectRepository)
    {
        _currentUser = currentUser;
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<ProjectMemberDto>> Handle(GetProjectMembersQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUser.UserId;
        if (currentUser is null)
            throw new ForbiddenException("User is no Authorized");
        var currentProject = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (currentProject is null)
            throw new NotFoundException("Project Not Found!");
        var isMember = currentProject.Members.Any(x => x.UserId == currentUser.Value);
        if (!isMember)
            throw new ForbiddenException("you are not a member of this project");
        var projectMembers = currentProject.Members.Select(x => new ProjectMemberDto(x.UserId, x.Role));
        return projectMembers;
    }
}