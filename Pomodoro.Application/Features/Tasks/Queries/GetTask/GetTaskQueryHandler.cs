using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Features.Tasks.Common;

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
        var task = await _context.GetTaskByIdAsync(query.Id, query.UserId, cancellationToken);
        if (task is null)
            throw new KeyNotFoundException($"Task of id:{query.Id} was not found.");
        return task.ToDto();
    }
}