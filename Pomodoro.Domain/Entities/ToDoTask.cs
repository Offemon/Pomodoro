using Pomodoro.Domain.Common.Interfaces;

namespace Pomodoro.Domain.Entities;

public class ToDoTask : IHasCreatedAt,IHasUpdatedAt
{
    private ToDoTask()
    {
        
    }

    public ToDoTask(
        Guid Id, Guid UserId, string Title, string? Description, int EstimatedPomodoros, DateTime? DueDate
        )
    {
        if (string.IsNullOrWhiteSpace(Title))
            throw new ArgumentException("Title cannot be null or whitespace");
        
        if (EstimatedPomodoros <= 0)
            throw new ArgumentException("Estimated Pomodoros must be greater than 0");
        this.Id = Id;
        this.UserId = UserId;
        this.IsCompleted = false;
        this.Title = Title;
        this.Description = Description;
        this.CompletedPomodoros = 0;
        this.EstimatedPomodoros = EstimatedPomodoros;
        this.CreatedAt = DateTime.UtcNow;   
        this.DueDate = DueDate;
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

    public void UpdateDetails(string title, string? description, int estimatedPomodoros ,DateTime? dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title cannot be empty");
        Title = title;
        Description = description;
        EstimatedPomodoros = estimatedPomodoros;
        DueDate = dueDate;
        UpdatedAt = DateTime.UtcNow;
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