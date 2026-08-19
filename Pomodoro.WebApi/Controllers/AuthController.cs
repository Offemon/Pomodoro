using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pomodoro.Application.Features.Authentication.Commands.LogInUser;
using Pomodoro.Application.Features.Authentication.Commands.RegisterUser;

namespace Pomodoro.WebApi.Controllers
{
    [AllowAnonymous]
    public sealed class AuthController : ApiControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command,
            CancellationToken cancellationToken)
        {
            var userId = await Mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Register), new { id = userId }, userId);
        }

        [HttpPost("login")]
        [ProducesResponseType<AuthResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(command, cancellationToken);
            return Ok(response);
        }
    }
}
