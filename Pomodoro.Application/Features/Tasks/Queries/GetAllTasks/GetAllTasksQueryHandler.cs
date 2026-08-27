using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Features.Tasks.Common;

namespace Pomodoro.Application.Features.Tasks.Queries.GetAllTasks;

public sealed class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, List<TaskDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllTasksQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<List<TaskDto>> Handle(GetAllTasksQuery query, CancellationToken cancellationToken)
    {
        var tasks = await _context.GetAllTasksForUserAsync(query.UserId, cancellationToken);
        return tasks.Select(t => t.ToDto()).ToList();
    }
}