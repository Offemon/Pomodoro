using Mediator;
using Dapper;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Features.Sessions.Common;
using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Sessions.Queries.GetQuickSessions;

public sealed class GetQuickSessionsQueryHandler : IRequestHandler<GetQuickSessionsQuery, List<SessionDto>>
{
    private readonly IApplicationDbContext _context;

    public GetQuickSessionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<List<SessionDto>> Handle(GetQuickSessionsQuery request, CancellationToken cancellationToken)
    {
        var sessionsEnumerable = await _context.Connection.QueryAsync<PomodoroSession>(SessionQueries.GetQuickSessions, new
        {
            UserId = request.UserId
        });
        var sessions = sessionsEnumerable.ToList();
        return sessions.Select(s => s.ToDto())
            .ToList();
    }
}