

namespace Pomodoro.Application.Features.Tasks.Queries;

public static class TaskQueries
{
    public const string GetActiveTasks = """
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
            WHERE "UserId" = @UserId AND "CurrentState" = @ActiveState;
        """;
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

    public const string GetTaskById = """
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
              WHERE "UserId" = @UserId AND "Id" = @TaskId;
          """;

    public const string GetActiveTasksPaged = """
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
                WHERE "UserId" = @UserId AND "CurrentState" = @ActiveState
                ORDER BY "CreatedAt" DESC
                LIMIT @Take OFFSET @Skip;

                SELECT COUNT(*)
                FROM todo_tasks
                WHERE "UserId" = @UserId;
            """;
    public const string GetAllTasksPaged = """
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
                WHERE "UserId" = @UserId
                ORDER BY "CreatedAt" DESC
                LIMIT @Take OFFSET @Skip;

                SELECT COUNT(*)
                FROM todo_tasks
                WHERE "UserId" = @UserId;
            """;
    
}