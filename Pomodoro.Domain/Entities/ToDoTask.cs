using Pomodoro.Domain.Common.Interfaces;
using Pomodoro.Domain.Enums;

namespace Pomodoro.Domain.Entities;

public class ToDoTask : IHasCreatedAt,IHasUpdatedAt
{
    private ToDoTask()
    {
        
    }

    public ToDoTask(
        Guid id, Guid userId, string title, string? description, int estimatedPomodoros, DateTime? dueDate, bool isPriority, TaskEnergyLevel taskEnergyLevel)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be null or whitespace");
        
        if (estimatedPomodoros <= 0)
            throw new ArgumentException("Estimated Pomodoros must be greater than 0");
        Id = id;
        UserId = userId;
        IsCompleted = false;
        Title = title;
        Description = description;
        CompletedPomodoros = 0;
        EstimatedPomodoros = estimatedPomodoros;
        CreatedAt = DateTime.UtcNow;   
        DueDate = dueDate;
        IsAbandoned = false;
        IsPriority = isPriority;
        EnergyLevel = taskEnergyLevel;
    }
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public bool IsCompleted { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int EstimatedPomodoros { get; private set; }
    public int CompletedPomodoros { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsAbandoned { get; private set; }
    public bool IsPriority { get; private set; }

    public TaskEnergyLevel EnergyLevel { get; private set; }

    public void Complete()
    {
        IsCompleted = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementCompletedPomodoros()
    {
        CompletedPomodoros++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Abandon()
    {
        if (IsCompleted)
            throw new InvalidOperationException("A completed task cannot be abandoned.");
        IsAbandoned = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void TogglePriority()
    {
        IsPriority = !IsPriority;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateEnergyLevel(TaskEnergyLevel newEnergyLevel)
    {
        EnergyLevel = newEnergyLevel;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string title, string? description, int estimatedPomodoros ,DateTime? dueDate, bool isPriority, TaskEnergyLevel taskEnergyLevel)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title cannot be empty");
        Title = title;
        Description = description;
        EstimatedPomodoros = estimatedPomodoros;
        DueDate = dueDate;
        UpdatedAt = DateTime.UtcNow;
        IsPriority = isPriority;
        EnergyLevel = taskEnergyLevel;
    }

    DateTime IHasCreatedAt.CreatedAt
    {
        get => CreatedAt;
    }

    DateTime? IHasUpdatedAt.UpdatedAt
    {
        get => UpdatedAt;
    }
}