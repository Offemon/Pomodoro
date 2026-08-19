using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Features.Sessions.Common;

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
        var sessions = await _context.GetQuickSessionsAsync(request.UserId, cancellationToken);
        return sessions.Select(s => s.ToDto())
            .ToList();
    }
}