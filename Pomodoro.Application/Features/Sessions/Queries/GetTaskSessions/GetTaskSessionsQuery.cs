using Mediator;
using Pomodoro.Application.Features.Sessions.Common;

namespace Pomodoro.Application.Features.Sessions.Queries.GetTaskSessions;

public sealed record GetTaskSessionsQuery(
        Guid UserId,
        Guid TaskId
    ) : IRequest<List<SessionDto>>;