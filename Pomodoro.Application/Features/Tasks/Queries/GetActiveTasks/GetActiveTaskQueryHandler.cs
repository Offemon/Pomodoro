using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Features.Tasks.Common;

namespace Pomodoro.Application.Features.Tasks.Queries.GetActiveTasks;

public sealed class GetActiveTaskQueryHandler : IRequestHandler<GetActiveTaskQuery, List<TaskDto>>
{
    private readonly IApplicationDbContext _context;

    public GetActiveTaskQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<List<TaskDto>> Handle(GetActiveTaskQuery request, CancellationToken cancellationToken)
    {
        var domainTasks = await _context.GetActiveTaskForUserAsync(request.UserId, cancellationToken);
        return domainTasks.Select(t => t.ToDto()).ToList();
    }
}