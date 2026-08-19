using Mediator;

namespace Pomodoro.Application.Features.Tasks.Commands.UpdateToDoTaskDetails;

public record UpdateToDoTaskDetailsCommand(
        Guid TaskId,
        Guid UserId,
        string Title,
        string? Description,
        int EstimatedPomodoros,
        DateTime? DueDate
    ) : IRequest;