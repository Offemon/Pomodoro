using Mediator;
using Pomodoro.Domain.Enums;

namespace Pomodoro.Application.Features.Tasks.Commands.UpdateToDoTaskDetails;

public record UpdateToDoTaskDetailsCommand(
        Guid TaskId,
        Guid UserId,
        string Title,
        string? Description,
        int EstimatedPomodoros,
        DateTime? DueDate,
        bool IsPriority,
        TaskEnergyLevel EnergyLevel
    ) : IRequest;