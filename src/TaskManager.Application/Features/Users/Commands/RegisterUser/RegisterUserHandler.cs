using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities.Aggregates;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }


    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existEmail = await _userRepository.GetByEmailAsync(request.Email);
        var passwordHash = _passwordHasher.HashPassword(request.Password);
        var user = User.CreateUser(request.Name, request.Email, passwordHash);
        if (existEmail is not null)
            throw new ConflictException("EmailAlready registered");

        await _userRepository.AddAsync(user);
        return user.Id;
    }
}