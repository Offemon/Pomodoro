using Mediator;
using Dapper;
using System.Diagnostics;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Common.Models;
using Pomodoro.Application.Features.Sessions.Common;
using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Sessions.Queries.GetTaskSessions;

public sealed class GetTaskSessionsQueryHandler : IRequestHandler<GetTaskSessionsQuery, PagedResult<SessionDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTaskSessionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<PagedResult<SessionDto>> Handle(GetTaskSessionsQuery request, CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("UserId", request.UserId);
        parameters.Add("TaskId", request.TaskId);
        SqlMapper.GridReader multiReader = await _context.Connection.QueryMultipleAsync(
            SessionQueries.GetTaskSessionsPaged, parameters);

        var sessionsEnumerable = await multiReader.ReadAsync<PomodoroSession>();
        int totalCount = await multiReader.ReadSingleAsync<int>();
        var sessions = sessionsEnumerable.ToList();
        List<SessionDto> sessionDtos = new List<SessionDto>();
        foreach (PomodoroSession session in sessions)
        {
            sessionDtos.Add(session.ToDto());
        }

        return new PagedResult<SessionDto>(
                items: sessionDtos,
                totalCount: totalCount,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize
            );
    }
}