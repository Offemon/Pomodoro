using Mediator;
using Pomodoro.Application.Common.Models;
using Pomodoro.Application.Features.Tasks.Common;

namespace Pomodoro.Application.Features.Tasks.Queries.GetAllTasks;

public sealed record GetAllTasksQuery(
        Guid UserId
    ) : PagedQueryParameters, IRequest<PagedResult<TaskDto>>;