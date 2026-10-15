using Mediator;
using Pomodoro.Application.Common.Models;
using Pomodoro.Application.Features.Sessions.Common;

namespace Pomodoro.Application.Features.Sessions.Queries.GetQuickSessions;

public sealed record GetQuickSessionsQuery(
        Guid UserId
    ) : PagedQueryParameters , IRequest<PagedResult<SessionDto>>;