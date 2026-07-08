using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Features.Users.DTOs;

namespace TaskManager.Application.Features.Users.Commands.LoginUser
{
    public record LoginUserCommand(string Email, string Password) : IRequest<AuthResultDto>;
}