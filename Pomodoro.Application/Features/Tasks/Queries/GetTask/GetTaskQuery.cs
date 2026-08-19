using Mediator;
using Pomodoro.Application.Features.Tasks.Common;

namespace Pomodoro.Application.Features.Tasks.Queries.GetTask;

public sealed record GetTaskQuery(
        Guid Id,
        Guid UserId
    ) : IRequest<TaskDto>;