using Dapper;
using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Features.Tasks.Common;
using Pomodoro.Domain.Entities;

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
        const string sql = TaskQueries.GetAllTasks;
        var tasksEnumerable = await _context.Connection.QueryAsync<ToDoTask>(sql, new
        {
            UserId = query.UserId
        });
        List<ToDoTask> tasks = tasksEnumerable.ToList();
        foreach (ToDoTask task in tasks)
        {
            task.SyncState();
            _context.UpdateEntity(task);
        }

        if (_context.HasActiveChanges)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        return tasks.Select(t => t.ToDto()).ToList();
    }
}