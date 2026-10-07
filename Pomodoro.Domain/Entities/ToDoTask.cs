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
        
        if (dueDate.HasValue && dueDate.Value < DateTime.UtcNow.Date.AddHours(1).AddSeconds(-10))
            throw new ArgumentException("The specified task completion deadline must be scheduled at least one hour into the future.");
        Id = id;
        UserId = userId;
        Title = title;
        Description = description;
        CompletedPomodoros = 0;
        OriginalEstimatedPomodoros = estimatedPomodoros;
        EstimatedPomodoros = estimatedPomodoros;
        OriginalDueDate = dueDate;
        DueDate = dueDate;
        DueDateDelayCount = 0;
        CurrentState = TaskState.Active;
        IsPriority = isPriority;
        EnergyLevel = taskEnergyLevel;
        ModifyCount = 0;
    }
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int OriginalEstimatedPomodoros { get; private set; }
    public int EstimatedPomodoros { get; private set; }
    public int CompletedPomodoros { get; private set; }
    public DateTime? OriginalDueDate { get; private set; }
    public DateTime? DueDate { get; private set; }
    public int DueDateDelayCount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public TaskState CurrentState { get; private set; }
    public bool IsPriority { get; private set; }
    public TaskEnergyLevel EnergyLevel { get; private set; }
    public int ModifyCount { get; private set; }

    public void Complete()
    {
        if (CurrentState == TaskState.Completed)
            return;
        if (CurrentState == TaskState.Abandoned)
            throw new InvalidOperationException("Cannot complete an abandoned task.");
        CurrentState = TaskState.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementCompletedPomodoros()
    {
        if (CurrentState == TaskState.Completed)
            throw new InvalidOperationException("Cannot perform operation on completed task");
        if (CurrentState == TaskState.Abandoned)
            throw new InvalidOperationException("Cannot perform operation on abandoned task.");
        CompletedPomodoros++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Abandon()
    {
        if (CurrentState == TaskState.Completed)
            throw new InvalidOperationException("A completed task cannot be abandoned.");
        if (CurrentState == TaskState.Abandoned)
            return;
        CurrentState = TaskState.Abandoned;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SyncState()
    {
        if (CurrentState == TaskState.Active && DueDate.HasValue && CompletedPomodoros > 0)
        {
            CurrentState = TaskState.Missed;
            UpdatedAt = DueDate.Value;
        }
        if (CurrentState == TaskState.Active && DueDate.HasValue && CompletedPomodoros == 0)
        {
            CurrentState = TaskState.Abandoned;
            UpdatedAt = DueDate.Value;
        }
    }

    public void TogglePriority()
    {
        if (CurrentState == TaskState.Completed)
            throw new InvalidOperationException("Cannot perform operation on completed task");
        if (CurrentState == TaskState.Abandoned)
            throw new InvalidOperationException("Cannot perform operation on abandoned task.");
        IsPriority = !IsPriority;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateDetails(string title, string? description, int estimatedPomodoros ,DateTime? dueDate, bool isPriority, TaskEnergyLevel taskEnergyLevel)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title cannot be empty");
        if (CurrentState == TaskState.Completed)
            throw new InvalidOperationException("Cannot perform operation on completed task");
        if (CurrentState == TaskState.Abandoned)
            throw new InvalidOperationException("Cannot perform operation on abandoned task.");
        if (dueDate.HasValue && dueDate.Value < DateTime.UtcNow.Date.AddHours(1).AddSeconds(-10))
            throw new ArgumentException("The specified task completion deadline must be scheduled at least one hour into the future.");
        if (OriginalDueDate == null && dueDate != null)
            OriginalDueDate = dueDate;
        if (DueDate != dueDate)
            DueDateDelayCount++;
        ModifyCount++;
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