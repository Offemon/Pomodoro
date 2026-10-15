using Dapper;
using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Common.Models;
using Pomodoro.Application.Features.Tasks.Common;
using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Tasks.Queries.GetAllTasks;

public sealed class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, PagedResult<TaskDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllTasksQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<PagedResult<TaskDto>> Handle(GetAllTasksQuery query, CancellationToken cancellationToken)
    {
        var multiReader = await _context.Connection.QueryMultipleAsync(TaskQueries.GetAllTasksPaged, new
        {
            UserId = query.UserId,
            Skip = query.Skip,
            Take = query.Take
        });
        var tasksEnumerable = await multiReader.ReadAsync<ToDoTask>();
        List<ToDoTask> tasks = tasksEnumerable.ToList();
        var totalCount = await multiReader.ReadSingleAsync<int>();
        foreach (ToDoTask task in tasks)
        {
            task.SyncState();
            _context.UpdateEntity(task);
        }

        if (_context.HasActiveChanges)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        var taskDtos = tasks.Select(t => t.ToDto()).ToList();
        return new PagedResult<TaskDto>(
            items: taskDtos,
            totalCount: totalCount,
            pageNumber: query.PageNumber,
            pageSize: query.PageSize
        );
    }
}