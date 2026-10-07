namespace Pomodoro.Application.Features.Tasks.Queries;

public static class TaskQueries
{
    public const string GetActiveTasks = @"
            SELECT
                id AS Id,
                user_id AS UserId,
                title AS Title,
                description AS Description,
                created_at AS CreatedAt,
                updated_at AS UpdatedAt,
                original_estimated_pomodoros AS OriginalEstimatedPomodoros,
                estimated_pomodoros AS EstimatedPomodoros,
                original_due_date AS OriginalDueDate,
                due_date AS DueDate,
                current_state AS CurrentState,
                is_priority AS IsPriority,
                energy_level AS EnergyLevel,
                modify_count AS ModifyCount
            FROM todo_tasks
            WHERE user_id = @UserId AND current_state = @ActiveState;
        ";
    public const string GetAllTasks = @"
            SELECT
                id AS Id,
                user_id AS UserId,
                title AS Title,
                description AS Description,
                created_at AS CreatedAt,
                updated_at AS UpdatedAt,
                original_estimated_pomodoros AS OriginalEstimatedPomodoros,
                estimated_pomodoros AS EstimatedPomodoros,
                original_due_date AS OriginalDueDate,
                due_date AS DueDate,
                current_state AS CurrentState,
                is_priority AS IsPriority,
                energy_level AS EnergyLevel,
                modify_count AS ModifyCount
            FROM todo_tasks
            WHERE user_id = @UserId;
        ";
}