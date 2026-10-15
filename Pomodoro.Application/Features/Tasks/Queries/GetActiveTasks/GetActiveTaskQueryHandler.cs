using System.Data;
using Dapper;
using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Common.Models;
using Pomodoro.Application.Features.Tasks.Common;
using Pomodoro.Domain.Entities;
using Pomodoro.Domain.Enums;

namespace Pomodoro.Application.Features.Tasks.Queries.GetActiveTasks;

public sealed class GetActiveTaskQueryHandler : IRequestHandler<GetActiveTaskQuery, PagedResult<TaskDto>>
{
    private readonly IApplicationDbContext _context;

    public GetActiveTaskQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<PagedResult<TaskDto>> Handle(GetActiveTaskQuery request, CancellationToken cancellationToken)
    {
        SqlMapper.GridReader multiReader = await _context.Connection.QueryMultipleAsync(TaskQueries.GetActiveTasksPaged, new
        {
            UserId = request.UserId,
            Take = request.Take,
            Skip = request.Skip
            
        });
        var tasksEnumerable = await multiReader.ReadAsync<ToDoTask>();
        List<ToDoTask> tasks = tasksEnumerable.ToList();

        int totalCount = await multiReader.ReadSingleAsync<int>();
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
        
        return new PagedResult<TaskDto>(
                items: activeTaskDtos,
                totalCount: totalCount,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize
            );
    }
}