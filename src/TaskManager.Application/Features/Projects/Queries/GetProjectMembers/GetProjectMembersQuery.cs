using MediatR;
using TaskManager.Application.Features.Tasks.DTOs;

namespace TaskManager.Application.Features.Tasks.Queries.GetProjectMembers;

public record GetProjectMembersQuery(Guid ProjectId) : IRequest<IEnumerable<ProjectMemberDto>>;