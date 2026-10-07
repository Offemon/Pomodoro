using Mediator;
using Dapper;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Features.Sessions.Common;
using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Sessions.Queries.GetAllSessions;

public sealed class GetAllSessionsQueryHandler : IRequestHandler<GetAllSessionsQuery, List<SessionDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllSessionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async ValueTask<List<SessionDto>> Handle(GetAllSessionsQuery request, CancellationToken cancellationToken)
    {
        var sessionsEnumerable = await _context.Connection.QueryAsync<PomodoroSession>(SessionQueries.GetAllSessions, new
        {
            UserId = request.UserId
        });
        var sessions = sessionsEnumerable.ToList();
        return sessions.Select(s => s.ToDto()).ToList();
    }
}