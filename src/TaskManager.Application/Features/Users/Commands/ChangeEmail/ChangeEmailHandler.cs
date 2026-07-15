using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Users.Commands.ChangeEmail;

public class ChangeEmailHandler : IRequestHandler<ChangeEmailCommand, Unit>
{
    private readonly ICurrentUserService _curentUserService;
    private readonly IUserRepository _userRepository;

    public ChangeEmailHandler(ICurrentUserService curentUserService, IUserRepository userRepository)
    {
        _curentUserService = curentUserService;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        var userId = _curentUserService.UserId;
        if (userId is null)
            throw new ForbiddenException("User is not authenticated.");
        var user = await _userRepository.GetByIdAsync(userId.Value);
        if (user == null)
            throw new NotFoundException("User Not found");
        user.ChangeEmail(request.NewEmail);
        await _userRepository.UpdateAsync(user);
        return Unit.Value;
    }
}