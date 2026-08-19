using Mediator;

namespace Pomodoro.Application.Features.Tasks.Commands.CreateToDoTask;

public sealed record CreateToDoTaskCommand(
        Guid UserId,
        string Title,
        string? Description,
        int EstimatedPomodoros,
        DateTime? DueDate
    ): IRequest<Guid>;