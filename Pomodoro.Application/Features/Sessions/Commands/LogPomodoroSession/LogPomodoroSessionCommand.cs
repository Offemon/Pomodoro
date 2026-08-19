using Mediator;

namespace Pomodoro.Application.Features.Sessions.Commands.LogPomodoroSession;

public sealed record LogPomodoroSessionCommand(
    Guid UserId,
    Guid? ToDoTaskId,
    int DurationMinutes
    ): IRequest<Guid>;