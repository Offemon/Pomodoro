using Mediator;
using Pomodoro.Application.Common.Models;
using Pomodoro.Application.Features.Tasks.Common;

namespace Pomodoro.Application.Features.Tasks.Queries.GetActiveTasks;

public sealed record GetActiveTaskQuery(Guid UserId) : PagedQueryParameters, IRequest<PagedResult<TaskDto>>;