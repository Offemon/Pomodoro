using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Tasks.Common;

public static class TaskExtensions
{
    public static TaskDto ToDto(this ToDoTask entity)
    {
        return new TaskDto(
                entity.Id,
                entity.Title,
                entity.Description,
                entity.CreatedAt,
                entity.IsCompleted,
                entity.EstimatedPomodoros,
                entity.CompletedPomodoros,
                entity.DueDate,
                entity.UpdatedAt
            );
    }
}