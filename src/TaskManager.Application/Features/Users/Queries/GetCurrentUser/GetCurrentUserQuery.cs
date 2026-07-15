using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.Features.Users.DTOs;

namespace TaskManager.Application.Features.Users.Queries.GetCurrentUser;

public record GetCurrentUserQuery() : IRequest<UserDTO>;