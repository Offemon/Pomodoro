using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Tasks.Common;

public static class TaskExtensions
{
    public static TaskDto ToDto(this ToDoTask entity)
    {
        return new TaskDto(
            Id: entity.Id,
            Title: entity.Title,
            Description: entity.Description,
            CreatedAt: entity.CreatedAt,
            IsCompleted: entity.IsCompleted,
            EstimatedPomodoros: entity.EstimatedPomodoros,
            CompletedPomodoros: entity.CompletedPomodoros,
            DueDate: entity.DueDate,
            UpdatedAt: entity.UpdatedAt,
            IsAbandoned: entity.IsAbandoned,
            IsPriority: entity.IsPriority,
            EnergyLevel: (int)entity.EnergyLevel
        );
    }
}