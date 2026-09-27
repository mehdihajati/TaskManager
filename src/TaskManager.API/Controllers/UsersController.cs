using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Contracts.Requests;
using TaskManager.Application.Features.Users.Commands.ChangeEmail;
using TaskManager.Application.Features.Users.Commands.ChangePassword;
using TaskManager.Application.Features.Users.Commands.DeleteAccount;
using TaskManager.Application.Features.Users.DTOs;
using TaskManager.Application.Features.Users.Queries.GetCurrentUser;
using TaskManager.Application.Features.Users.Queries.GetUserById;

namespace TaskManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var query = new GetCurrentUserQuery();
        UserDTO queryResult = await _mediator.Send(query);
        return Ok(queryResult);
    }
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var query = new GetUserByIdQuery(userId);
        UserDTO queryResult = await _mediator.Send(query);
        return Ok(queryResult);
    }
    [HttpPatch("me/email")]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest request)
    {
        var command = new ChangeEmailCommand(request.NewEmail);
        await _mediator.Send(command);
        return NoContent();
    }
    [HttpPatch("me/password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var command = new ChangePasswordCommand(request.CurrentPassword, request.NewPassword);
        await _mediator.Send(command);
        return NoContent();
    }
    [HttpDelete("me")]
    public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccountRequest request)
    {
        var command = new DeleteAccountCommand(request.CurrentPassword);
        await _mediator.Send(command);
        return NoContent();
    }

}
