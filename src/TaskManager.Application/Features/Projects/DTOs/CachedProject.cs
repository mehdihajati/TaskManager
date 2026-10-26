using TaskManager.Application.Features.Tasks.DTOs;

namespace TaskManager.Application.Features.Projects.DTOs;

public record CachedProject(ProjectDTO Project, List<ProjectMemberDto> Members);