using Mediator;
using Pomodoro.Application.Features.Tasks.Common;

namespace Pomodoro.Application.Features.Tasks.Queries.GetAllTasks;

public sealed record GetAllTasksQuery(
        Guid UserId
    ) : IRequest<List<TaskDto>>;