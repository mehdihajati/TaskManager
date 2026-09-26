using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Contracts.Requests;
using TaskManager.Application.Features.Users.Commands.LoginUser;
using TaskManager.Application.Features.Users.Commands.RegisterUser;
using TaskManager.Application.Features.Users.DTOs;

namespace TaskManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
    {
        var command = LoginUserCommand.Create(request.Email, request.Password);
        AuthResultDto result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpPost("register"), AllowAnonymous]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest request)
    {
        var command = new RegisterUserCommand(request.Name, request.Email, request.Password);
        Guid userId = await _mediator.Send(command);
        return Created(string.Empty, new { Id = userId });
    }

}
