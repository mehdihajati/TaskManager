using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Users.Commands.DeleteAccount;

public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IPasswordHasher _passwordHasher;

    public DeleteAccountHandler(IUserRepository userRepository, ICurrentUserService currentUserService, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _passwordHasher = passwordHasher;

    }

    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId is null)
            throw new ForbiddenException("Unauthorized to delete account");
        var user = await _userRepository.GetByIdAsync(userId.Value);

        if (user == null)
            throw new NotFoundException("User Not found");
        var isPasswordValid = _passwordHasher.VerifyPassword(request.CurrentPassword, user.PasswordHash);
        if (!isPasswordValid)
            throw new ForbiddenException("Password is not valid");

        user.DeleteAccount();
        await _userRepository.UpdateAsync(user);
        return Unit.Value;
    }
}