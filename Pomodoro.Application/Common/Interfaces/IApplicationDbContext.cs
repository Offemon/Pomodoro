using Pomodoro.Domain.Entities;
namespace Pomodoro.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    IQueryable<User> Users { get; }
    IQueryable<ToDoTask> ToDoTasks { get; }
    IQueryable<PomodoroSession> PomodoroSessions { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<bool> UserExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> TaskExistsForUserAsync(Guid taskId, Guid userId, CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);
    Task<List<ToDoTask>> GetActiveTaskForUserAsync(Guid userId, CancellationToken cancellationToken= default);
    Task<ToDoTask?> GetTaskByIdAsync(Guid taskId, Guid userId, CancellationToken cancellationToken = default);
    Task<List<PomodoroSession>> GetTaskSessionsAsync(Guid taskId, Guid userId, CancellationToken cancellationToken = default);
    Task<List<PomodoroSession>> GetAllSessionsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<List<PomodoroSession>> GetQuickSessionsAsync(Guid userId, CancellationToken cancellationToken = default);
    void AddEntity<TEntity>(TEntity entity) where TEntity : class;
    void RemoveEntity<TEntity>(TEntity entity) where TEntity : class;
}