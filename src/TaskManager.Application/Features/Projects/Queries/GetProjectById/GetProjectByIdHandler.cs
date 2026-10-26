using MediatR;
using TaskManager.Application.Common;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Features.Projects.DTOs;
using TaskManager.Application.Features.Tasks.DTOs;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Tasks.Queries.GetProjectById;

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ProjectDTO>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly ICacheService _cache;

    public GetProjectByIdHandler(IProjectRepository projectRepository, ICurrentUserService currentUser, ICacheService cache)
    {
        _projectRepository = projectRepository;
        _currentUser = currentUser;
        _cache = cache;
    }

    public async Task<ProjectDTO> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUser.UserId;
        if (currentUser is null)
            throw new ForbiddenException("User is not authenticated");
        var key = CacheKeys.Project(request.ProjectId);
        var cached = await _cache.GetAsync<CachedProject>(key, cancellationToken);

        if (cached is null)
        {

            var currentProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (currentProject is null)
                throw new NotFoundException("Project not found");
            var dto = new ProjectDTO(currentProject.Id, currentProject.OwnerId, currentProject.Name, currentProject.Description, currentProject.Status, currentProject.Deadline);

            var members = currentProject.Members.Where(x => !x.IsDeleted)
                                                .Select(x => new ProjectMemberDto(x.UserId, x.Role))
                                                .ToList();
            cached = new CachedProject(dto, members);
            await _cache.SetAsync(key, cached, cancellationToken: cancellationToken);

        }
        if (!cached.Members.Any(x => x.UserId == currentUser.Value))
            throw new ForbiddenException("You Are not a member of this Project");
        return cached.Project;
    }
}