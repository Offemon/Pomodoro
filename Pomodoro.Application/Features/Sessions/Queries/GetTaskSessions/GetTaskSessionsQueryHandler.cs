using Mediator;
using Dapper;
using System.Diagnostics;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Features.Sessions.Common;
using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Sessions.Queries.GetTaskSessions;

public sealed class GetTaskSessionsQueryHandler : IRequestHandler<GetTaskSessionsQuery, List<SessionDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTaskSessionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<List<SessionDto>> Handle(GetTaskSessionsQuery request, CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("UserId", request.UserId);
        parameters.Add("TaskId", request.TaskId);
        Debug.WriteLine(request.UserId);
        Debug.WriteLine(request.TaskId);
        var sessionsEnumerable = await _context.Connection.QueryAsync<PomodoroSession>(SessionQueries.GetTaskSessions, parameters);
        var sessions = sessionsEnumerable.ToList();
        return sessions
            .Select(s => s.ToDto()).ToList();
    }
}