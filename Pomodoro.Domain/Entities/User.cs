namespace Pomodoro.Domain.Entities;

public class User
{
    public User()
    {
        
    }
    public User(Guid id, string email, string passwordHash)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
    }
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    
    public ICollection<ToDoTask> Tasks { get; private set; } = new List<ToDoTask>();
    public ICollection<PomodoroSession> Sessions { get; private set; } = new List<PomodoroSession>();
}