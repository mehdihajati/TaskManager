using MediatR;
using TaskManager.Application.Features.Users.DTOs;

namespace TaskManager.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDTO>;