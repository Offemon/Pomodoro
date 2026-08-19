using Pomodoro.Domain.Common.Interfaces;

namespace Pomodoro.Domain.Entities;

public class PomodoroSession : IHasCreatedAt
{
    private PomodoroSession()
    {
    }

    public PomodoroSession(Guid id,
        Guid userId,
        Guid? toDoTaskId,
        int durationMinutes)
    {
        if (durationMinutes <= 0)
            throw new ArgumentException("Duration must be greater than 0");
        
        this.Id = id;
        this.UserId = userId;
        this.ToDoTaskId = toDoTaskId;
        this.DurationMinutes = durationMinutes;
        this.CreatedAt = DateTime.UtcNow;
        this.CompletedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid? ToDoTaskId { get; private set; }
    public int DurationMinutes { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime CompletedAt { get; private set; }

    DateTime IHasCreatedAt.CreatedAt
    {
        get => CreatedAt;
    }
}