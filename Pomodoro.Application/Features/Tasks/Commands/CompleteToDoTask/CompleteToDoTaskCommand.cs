using Mediator;

namespace Pomodoro.Application.Features.Tasks.Commands.CompleteToDoTask;

public sealed record CompleteToDoTaskCommand(
        Guid TaskId,
        Guid UserId
    ): IRequest;