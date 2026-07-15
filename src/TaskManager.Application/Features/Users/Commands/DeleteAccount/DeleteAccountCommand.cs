using MediatR;

namespace TaskManager.Application.Features.Users.Commands.DeleteAccount;

public record DeleteAccountCommand(string CurrentPassword) : IRequest<Unit>;