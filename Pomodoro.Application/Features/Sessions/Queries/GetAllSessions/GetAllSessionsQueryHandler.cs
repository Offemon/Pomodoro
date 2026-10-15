using Mediator;
using Dapper;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Common.Models;
using Pomodoro.Application.Features.Sessions.Common;
using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Sessions.Queries.GetAllSessions;

public sealed class GetAllSessionsQueryHandler : IRequestHandler<GetAllSessionsQuery, PagedResult<SessionDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllSessionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async ValueTask<PagedResult<SessionDto>> Handle(GetAllSessionsQuery request, CancellationToken cancellationToken)
    {
        SqlMapper.GridReader multiReader = await _context.Connection.QueryMultipleAsync(SessionQueries.GetAllSessionsPaged, new
        {
            UserId = request.UserId,
            Take = request.Take,
            Skip = request.Skip
        });
        var sessionsEnumerable = await multiReader.ReadAsync<PomodoroSession>();
        var sessions = sessionsEnumerable.ToList();
        List<SessionDto> sessionDtos = new List<SessionDto>();
        foreach (var session in sessions)
        {
            sessionDtos.Add(session.ToDto());
        }
        int totalCount = await multiReader.ReadSingleAsync<int>();
        return new PagedResult<SessionDto>(
                items: sessionDtos,
                totalCount: totalCount,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize
            );
    }
}