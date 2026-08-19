using Mediator;
using Pomodoro.Application.Features.Sessions.Common;

namespace Pomodoro.Application.Features.Sessions.Queries.GetQuickSessions;

public sealed record GetQuickSessionsQuery(
        Guid UserId
    ) : IRequest<List<SessionDto>>;