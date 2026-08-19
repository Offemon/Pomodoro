using Mediator;
namespace Pomodoro.Application.Features.Tasks.Commands.IncrementTaskPomodoro;

public sealed record IncrementTaskPomodoroCommand(
    Guid TaskId,
    Guid UserId
    ):IRequest;