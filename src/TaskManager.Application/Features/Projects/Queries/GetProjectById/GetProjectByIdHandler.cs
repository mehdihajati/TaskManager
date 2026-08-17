using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Features.Projects.DTOs;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Projects.Queries.GetProjectById;

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ProjectDTO>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentUserService _currentUser;

    public GetProjectByIdHandler(IProjectRepository projectRepository, ICurrentUserService currentUser)
    {
        _projectRepository = projectRepository;
        _currentUser = currentUser;
    }

    public async Task<ProjectDTO> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var currentProject = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (currentProject is null)
            throw new NotFoundException("Project not found");
        var currentUser = _currentUser.UserId;
        if (currentUser is null)
            throw new ForbiddenException("User is not authenticated");
        var requesterMember = currentProject.Members.FirstOrDefault(x => x.UserId == currentUser.Value);
        if (requesterMember is null)
            throw new ForbiddenException("You are not a member of this project");
        return new ProjectDTO(request.ProjectId, currentProject.OwnerId, currentProject.Name, currentProject.Description, currentProject.Status, currentProject.Deadline);
    }
}