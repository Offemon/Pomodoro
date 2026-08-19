using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Features.Sessions.Common;

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

        var sessions = await _context.GetTaskSessionsAsync(request.TaskId, request.UserId, cancellationToken);
        return sessions
            .Select(s => s.ToDto()).ToList();
    }
}