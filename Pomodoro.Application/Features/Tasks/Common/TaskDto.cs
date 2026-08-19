namespace Pomodoro.Application.Features.Tasks.Common;

public record TaskDto(
        Guid Id,
        string Title,
        string? Description,
        DateTime CreatedAt,
        bool IsCompleted,
        int EstimatedPomodoros,
        int CompletedPomodoros,
        DateTime? DueDate,
        DateTime? UpdatedAt
    );