using Mediator;
using Pomodoro.Application.Features.Sessions.Common;

namespace Pomodoro.Application.Features.Sessions.Queries.GetAllSessions;

public record GetAllSessionsQuery(
        Guid UserId
    ) : IRequest<List<SessionDto>>;