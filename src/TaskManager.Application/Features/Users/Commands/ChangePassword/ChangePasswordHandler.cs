using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPasswordHasher _passwordHasher;

        public ChangePasswordHandler(IUserRepository userRepository, ICurrentUserService currentUserService, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (userId is null)
                throw new ForbiddenException("User id can not be nulled");
            var user = await _userRepository.GetByIdAsync(userId.Value);
            if (user == null)
                throw new NotFoundException("User can not be null");
            user.ChangePassword(_passwordHasher.HashPassword(request.CurrentPassword), _passwordHasher.HashPassword(request.NewPassword));
            await _userRepository.UpdateAsync(user);
            return Unit.Value;
        }
    }
}