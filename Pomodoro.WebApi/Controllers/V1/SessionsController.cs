using Microsoft.AspNetCore.Mvc;
using Pomodoro.Application.Common.Models;
using Pomodoro.Application.Features.Sessions.Commands.LogPomodoroSession;
using Pomodoro.Application.Features.Sessions.Common;
using Pomodoro.Application.Features.Sessions.Queries.GetAllSessions;
using Pomodoro.Application.Features.Sessions.Queries.GetQuickSessions;
using Pomodoro.Application.Features.Sessions.Queries.GetTaskSessions;
using Pomodoro.WebApi.Extensions;

namespace Pomodoro.WebApi.Controllers.V1
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
        [ProducesResponseType<PagedResult<SessionDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSessions([FromQuery] int pageNumber,[FromQuery] int pageSize,CancellationToken cancellationToken)
        {
            var verifiedUserId = User.GetUserId();
            GetAllSessionsQuery query = new GetAllSessionsQuery(verifiedUserId)
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            var response = await Mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        [HttpGet("quick")]
        [ProducesResponseType<PagedResult<SessionDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuickSessions([FromQuery] int pageNumber,[FromQuery] int pageSize,CancellationToken cancellationToken)
        {
            var verifiedUserId = User.GetUserId();
            GetQuickSessionsQuery query = new GetQuickSessionsQuery(verifiedUserId)
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            var response = await Mediator.Send(new GetQuickSessionsQuery(verifiedUserId), cancellationToken);
            return Ok(response);
        }

        [HttpGet("{taskId:guid}")]
        [ProducesResponseType<PagedResult<SessionDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTaskSessions([FromRoute] Guid taskId, [FromQuery] int pageNumber,[FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            var verifiedUser = User.GetUserId();
            GetTaskSessionsQuery query = new GetTaskSessionsQuery(
                UserId: verifiedUser,
                TaskId: taskId
            )
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            var response = await Mediator.Send(query, cancellationToken);
            return Ok(response);
        }
        
    }
}
