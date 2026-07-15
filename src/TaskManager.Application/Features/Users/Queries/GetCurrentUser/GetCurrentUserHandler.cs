using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Features.Users.DTOs;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Users.Queries.GetCurrentUser;

public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, UserDTO>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    public GetCurrentUserHandler(ICurrentUserService currentUserService, IUserRepository userRepository)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
    }

    public async Task<UserDTO> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId is null)
            throw new ForbiddenException("User is not authenticated.");
        var user = await _userRepository.GetByIdAsync(userId.Value);
        if (user is null)
            throw new NotFoundException("User Not found");
        return new UserDTO(user.Id, user.Name, user.Email, user.CreatedAt);
    }
}