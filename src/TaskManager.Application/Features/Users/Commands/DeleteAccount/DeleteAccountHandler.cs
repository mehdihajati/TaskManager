using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManager.Application.Features.Users.Commands.DeleteAccount;

public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;


    public DeleteAccountHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;

    }

    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId is null)
            throw new ForbiddenException("Unauthorized to delete account");
        var user = await _userRepository.GetByIdAsync(userId.Value);
        if (user == null)
            throw new NotFoundException("User Not found");

        try
        {
            user.DeleteAccount();
            await _userRepository.UpdateAsync(user);
        }
        catch (InvalidCastException ex)
        {
            throw new InvalidOperationException("Error deleting account: " + ex.Message);
        }
        return Unit.Value;
    }
}