using MediatR;
using TaskManager.Application.Features.Users.DTOs;

namespace TaskManager.Application.Features.Users.Commands.LoginUser;

public record LoginUserCommand(string Email, string Password) : IRequest<AuthResultDto>
{
    public static LoginUserCommand Create(string email, string password)
        => new(email.Trim().ToLowerInvariant(), password);
}