using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Features.Sessions.Common;

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
        var sessions = await _context.GetAllSessionsAsync(request.UserId, cancellationToken);
        return sessions.Select(s => s.ToDto()).ToList();
    }
}