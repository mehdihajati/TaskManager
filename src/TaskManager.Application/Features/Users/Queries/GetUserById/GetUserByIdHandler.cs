using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Features.Users.DTOs;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user is null)
                throw new NotFoundException("User Not found");
            return new UserDTO(user.Id, user.Name, user.Email, user.CreatedAt);
        }
    }
}