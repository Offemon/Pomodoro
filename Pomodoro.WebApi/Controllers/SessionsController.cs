using Microsoft.AspNetCore.Mvc;
using Pomodoro.Application.Features.Sessions.Commands.LogPomodoroSession;
using Pomodoro.Application.Features.Sessions.Common;
using Pomodoro.Application.Features.Sessions.Queries.GetAllSessions;
using Pomodoro.Application.Features.Sessions.Queries.GetQuickSessions;
using Pomodoro.Application.Features.Sessions.Queries.GetTaskSessions;
using Pomodoro.WebApi.Extensions;

namespace Pomodoro.WebApi.Controllers
{
    public class SessionsController : ApiControllerBase
    {
        [HttpPost("log-session")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LogSession([FromBody] LogPomodoroSessionCommand command,
            CancellationToken cancellationToken)
        {
            var verifiedUserId = User.GetUserId();
            command.VerifiedUserId = verifiedUserId;
            var sessionId = await Mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(LogSession), new { id = sessionId }, sessionId);
        }

        [HttpGet]
        [ProducesResponseType<IEnumerable<SessionDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSessions(CancellationToken cancellationToken)
        {
            var verifiedUserId = User.GetUserId();
            var response = await Mediator.Send(new GetAllSessionsQuery(verifiedUserId), cancellationToken);
            return Ok(response);
        }

        [HttpGet("quick")]
        [ProducesResponseType<IEnumerable<SessionDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuickSessions(CancellationToken cancellationToken)
        {
            var verifiedUserId = User.GetUserId();
            var response = await Mediator.Send(new GetQuickSessionsQuery(verifiedUserId), cancellationToken);
            return Ok(response);
        }

        [HttpGet("task/{taskId:guid}")]
        [ProducesResponseType<IEnumerable<SessionDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTaskSessions([FromRoute] Guid taskId, CancellationToken cancellationToken)
        {
            var verifiedUser = User.GetUserId();
            var response = await Mediator.Send(new GetTaskSessionsQuery(taskId, verifiedUser), cancellationToken);
            return Ok(response);
        }
        
    }
}
