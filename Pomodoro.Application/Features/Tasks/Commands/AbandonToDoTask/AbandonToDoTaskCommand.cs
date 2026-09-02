using Mediator;

namespace Pomodoro.Application.Features.Tasks.Commands.AbandonToDoTask;

public sealed record AbandonToDoTaskCommand(
        Guid TaskId,
        Guid UserId
    ) : IRequest;