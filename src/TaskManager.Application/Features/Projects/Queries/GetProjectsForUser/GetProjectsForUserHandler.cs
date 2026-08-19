using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Features.Projects.DTOs;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Projects.Queries.GetProjectsForUser;

public class GetProjectsForUserHandler : IRequestHandler<GetProjectsForUserQuery, IEnumerable<ProjectDTO>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentUserService _currentUser;

    public GetProjectsForUserHandler(IProjectRepository projectRepository, ICurrentUserService currentUser)
    {
        _projectRepository = projectRepository;
        _currentUser = currentUser;
    }

    public async Task<IEnumerable<ProjectDTO>> Handle(GetProjectsForUserQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUser.UserId;
        if (currentUser == null)
            throw new ForbiddenException("User is not authonticated!");
        var usersProject = await _projectRepository.GetUserProjectsAsync(currentUser.Value);
        var projectDTOs = usersProject.Select(x => new ProjectDTO(x.Id, x.OwnerId, x.Name, x.Description, x.Status, x.Deadline));
        return projectDTOs;

    }
}