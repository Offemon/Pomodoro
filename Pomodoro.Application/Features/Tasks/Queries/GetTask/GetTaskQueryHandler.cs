using Mediator;
using Dapper;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Features.Tasks.Common;
using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Tasks.Queries.GetTask;

public sealed class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, TaskDto>
{
    private readonly IApplicationDbContext _context;

    public GetTaskQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<TaskDto> Handle(GetTaskQuery query, CancellationToken cancellationToken)
    {
        var task = await _context.Connection.QuerySingleOrDefaultAsync<ToDoTask>(TaskQueries.GetTaskById, new
        {
            UserId = query.UserId,
            TaskId = query.Id
        });
        if (task is null)
            throw new KeyNotFoundException($"Task of id:{query.Id} was not found.");
        return task.ToDto();
    }
}