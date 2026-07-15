using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities.Aggregates;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IProjectRepository _projectRepository;

    public CreateProjectHandler(ICurrentUserService currentUserService, IProjectRepository projectRepository)
    {
        _currentUserService = currentUserService;
        _projectRepository = projectRepository;
    }

    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var ownerId = _currentUserService.UserId;
        if (ownerId is null)
            throw new ForbiddenException("owner id can not be null");
        var createProject = Project.CreateProject(request.Name, request.Description, ownerId.Value, request.Deadline);
        await _projectRepository.AddAsync(createProject);
        return createProject.Id;

    }
}