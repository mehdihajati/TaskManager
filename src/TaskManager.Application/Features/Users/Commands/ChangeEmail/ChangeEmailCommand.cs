using MediatR;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Users.Commands.ChangeEmail;

public record ChangeEmailCommand(string NewEmail) : IRequest<Unit>;