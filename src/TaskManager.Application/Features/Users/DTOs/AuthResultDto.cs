using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Application.Features.Users.DTOs;

public record AuthResultDto(string Token, Guid UserId, string Name);