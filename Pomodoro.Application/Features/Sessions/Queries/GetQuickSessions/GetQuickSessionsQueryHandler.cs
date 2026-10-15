using Mediator;
using Dapper;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Common.Models;
using Pomodoro.Application.Features.Sessions.Common;
using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Sessions.Queries.GetQuickSessions;

public sealed class GetQuickSessionsQueryHandler : IRequestHandler<GetQuickSessionsQuery, PagedResult<SessionDto>>
{
    private readonly IApplicationDbContext _context;

    public GetQuickSessionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<PagedResult<SessionDto>> Handle(GetQuickSessionsQuery request, CancellationToken cancellationToken)
    {
        SqlMapper.GridReader multiReader =
            await _context.Connection.QueryMultipleAsync(SessionQueries.GetQuickSessionsPaged, new
            {
                UserId = request.UserId,
                Take = request.Take,
                Skip = request.Skip
            });

        var sessionsEnumerable = await multiReader.ReadAsync<PomodoroSession>();
        var sessions = sessionsEnumerable.ToList();
        List<SessionDto> sessionsDto = new List<SessionDto>();
        foreach (PomodoroSession session in sessions)
        {
            sessionsDto.Add(session.ToDto());
        }
        int totalCount = await multiReader.ReadSingleAsync<int>();
        return new PagedResult<SessionDto>(
                items: sessionsDto,
                totalCount: totalCount,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize
            );
    }
}