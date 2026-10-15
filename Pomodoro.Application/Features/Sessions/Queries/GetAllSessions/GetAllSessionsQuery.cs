using Mediator;
using Pomodoro.Application.Common.Models;
using Pomodoro.Application.Features.Sessions.Common;

namespace Pomodoro.Application.Features.Sessions.Queries.GetAllSessions;

public record GetAllSessionsQuery(
        Guid UserId
    ) : PagedQueryParameters ,IRequest<PagedResult<SessionDto>>;