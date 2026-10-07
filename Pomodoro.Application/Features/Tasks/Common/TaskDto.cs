namespace Pomodoro.Application.Features.Tasks.Common;

public record TaskDto(
        Guid Id,
        string Title,
        string? Description,
        DateTime CreatedAt,
        int CurrentState,
        int EstimatedPomodoros,
        int CompletedPomodoros,
        DateTime? DueDate,
        DateTime? UpdatedAt,
        bool IsPriority,
        int EnergyLevel,
        int ModifyCount
    );