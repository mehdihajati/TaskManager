using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Users.Commands.LoginUser;
using TaskManager.Application.Features.Users.DTOs;

namespace TaskManager.API.Controllers
{
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
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = LoginUserCommand.Create(request.Email, request.Password);
            AuthResultDto result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
