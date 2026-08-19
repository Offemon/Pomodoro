using Mediator;

namespace Pomodoro.Application.Features.Tasks.Commands.DeleteToDoTask;

public sealed record DeleteToDoTaskCommand(
        Guid TaskId,
        Guid UserId
    ) : IRequest;