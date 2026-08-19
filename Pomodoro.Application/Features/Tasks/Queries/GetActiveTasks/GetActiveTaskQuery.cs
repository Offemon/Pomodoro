using Mediator;
using Pomodoro.Application.Features.Tasks.Common;

namespace Pomodoro.Application.Features.Tasks.Queries.GetActiveTasks;

public sealed record GetActiveTaskQuery(Guid UserId) : IRequest<List<TaskDto>>;