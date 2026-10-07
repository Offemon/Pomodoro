using System.Data;
using Dapper;
using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Features.Tasks.Common;
using Pomodoro.Domain.Entities;
using Pomodoro.Domain.Enums;

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
        var tasksEnumerable = await _context.Connection.QueryAsync<ToDoTask>(TaskQueries.GetActiveTasks, new
        {
            UserId = request.UserId,
            ActiveState = (int)TaskState.Active
        });
        
        var tasks = tasksEnumerable.ToList();
        var activeTaskDtos = new List<TaskDto>();
        foreach (ToDoTask task in tasks)
        {
            task.SyncState();
            if (task.CurrentState == TaskState.Active)
                activeTaskDtos.Add(task.ToDto());
            else
                _context.UpdateEntity(task);
        }

        if (_context.HasActiveChanges)
            await _context.SaveChangesAsync(cancellationToken);
        
        return activeTaskDtos;
    }
}