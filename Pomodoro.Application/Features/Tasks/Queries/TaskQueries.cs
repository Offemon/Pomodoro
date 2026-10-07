

namespace Pomodoro.Application.Features.Tasks.Queries;

public static class TaskQueries
{
    public const string GetActiveTasks = @"
            SELECT
                ""Id"",
                ""UserId"",
                ""Title"",
                ""Description"",
                ""CreatedAt"",
                ""UpdatedAt"",
                ""OriginalEstimatedPomodoros"",
                ""EstimatedPomodoros"",
                ""OriginalDueDate"",
                ""DueDate"",
                ""CurrentState"",
                ""IsPriority"",
                ""EnergyLevel"",
                ""ModifyCount""
            FROM todo_tasks
            WHERE ""UserId"" = @UserId AND ""CurrentState"" = @ActiveState;
        ";
    public const string GetAllTasks = """
            SELECT
                "Id",
                "UserId",
                "Title",
                "Description",
                "CreatedAt",
                "UpdatedAt",
                "OriginalEstimatedPomodoros",
                "EstimatedPomodoros",
                "OriginalDueDate",
                "DueDate",
                "CurrentState",
                "IsPriority",
                "EnergyLevel",
                "ModifyCount"
            FROM todo_tasks
            WHERE "UserId" = @UserId;
        """;
}