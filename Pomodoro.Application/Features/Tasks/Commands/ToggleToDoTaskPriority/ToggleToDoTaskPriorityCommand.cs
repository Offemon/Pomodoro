using Mediator;

namespace Pomodoro.Application.Features.Tasks.Commands.ToggleToDoTaskPriority;

public record ToggleToDoTaskPriorityCommand(
    Guid TaskId,
    Guid UserId
) : IRequest;