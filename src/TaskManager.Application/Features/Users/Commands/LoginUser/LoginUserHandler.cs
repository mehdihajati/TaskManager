using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Features.Users.DTOs;
using TaskManager.Domain.Entities.Aggregates;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Users.Commands.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, AuthResultDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public LoginUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    async Task<AuthResultDto> IRequestHandler<LoginUserCommand, AuthResultDto>.Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        var isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
        if (user is null)
            throw new NotFoundException("Invalid email or password");
        if (!isPasswordValid)
            throw new NotFoundException("Invalid username or password");
        var token = _jwtService.GenerateToken(user.Id, user.Email);
        return new AuthResultDto(token, user.Id, user.Name);
    }
}