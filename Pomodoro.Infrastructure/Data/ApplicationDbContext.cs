using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Domain.Common.Interfaces;
using Pomodoro.Domain.Entities;
using Pomodoro.Domain.Enums;

namespace Pomodoro.Infrastructure.Data;

public class ApplicationDbContext : DbContext,IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public IQueryable<User> Users => Set<User>();
    public IQueryable<ToDoTask> ToDoTasks => Set<ToDoTask>();
    public IQueryable<PomodoroSession> PomodoroSessions => Set<PomodoroSession>();

    public IDbConnection Connection => Database.GetDbConnection();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<DateTime>()
            .HaveConversion<UtcDateTimeConverter>();

        configurationBuilder
            .Properties<DateTime?>()
            .HaveConversion<NullableUtcDateTimeConverter>();
        base.ConfigureConventions(configurationBuilder);
    }

    private sealed class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
    {
        public UtcDateTimeConverter() : base(
                v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            )
        {
            
        }
    }

    private sealed class NullableUtcDateTimeConverter : ValueConverter<DateTime?, DateTime?>
    {
        public NullableUtcDateTimeConverter() : base(
                v => !v.HasValue ? v : (v.Value.Kind == DateTimeKind.Utc ? v : v.Value.ToUniversalTime()),
                v => !v.HasValue ? v : DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)
            )
        {
            
        }
    }

    public bool HasActiveChanges => ChangeTracker.HasChanges();

    public void UpdateEntity<TEntity>(TEntity entity) where TEntity : class
    {
        base.Update(entity);
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added && entry.Entity is IHasCreatedAt)
            {
                entry.Property(nameof(IHasCreatedAt.CreatedAt)).CurrentValue = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified && entry.Entity is IHasUpdatedAt)
            {
                entry.Property(nameof(IHasUpdatedAt.UpdatedAt)).CurrentValue = DateTime.UtcNow;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.ToLower().Trim();
        return !await Set<User>().AnyAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);
    }
    public async Task<bool> UserExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Set<User>().AnyAsync(u => u.Id == id, cancellationToken);
    }
    public async Task<bool> TaskExistsForUserAsync(Guid taskId, Guid userId, CancellationToken cancellationToken = default)
    {
        return await Set<ToDoTask>().AnyAsync(t => t.Id == taskId && t.UserId == userId, cancellationToken);
    }

    public async Task<List<ToDoTask>> GetActiveTaskForUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await Set<ToDoTask>()
            .Where(t => t.UserId == userId && t.CurrentState == TaskState.Active)
            .OrderBy(t => t.DueDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<ToDoTask>> GetAllTasksForUserAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await Set<ToDoTask>()
            .Where(t => t.UserId == userId)
            .OrderBy(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<ToDoTask?> GetTaskByIdAsync(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await Set<ToDoTask>().FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId, cancellationToken);
    }

    public async Task<List<PomodoroSession>> GetTaskSessionsAsync(Guid taskId, Guid userId, CancellationToken cancellationToken)
    {
        return await Set<PomodoroSession>()
            .Where(s => s.ToDoTaskId == taskId && s.UserId == userId)
            .OrderByDescending(s => s.CompletedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<PomodoroSession>> GetAllSessionsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Set<PomodoroSession>()
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.CompletedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<PomodoroSession>> GetQuickSessionsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Set<PomodoroSession>()
            .Where(s => s.ToDoTaskId == null)
            .OrderByDescending(s => s.CompletedAt)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Set<User>().FirstOrDefaultAsync(u => u.Email == email.ToLower().Trim(), cancellationToken);
    }
    public void AddEntity<TEntity>(TEntity entity) where TEntity : class
    {
        Set<TEntity>().Add(entity);
    }

    public void RemoveEntity<TEntity>(TEntity entity) where TEntity : class
    {
        Set<TEntity>().Remove(entity);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await Database.BeginTransactionAsync(cancellationToken);
    }
}