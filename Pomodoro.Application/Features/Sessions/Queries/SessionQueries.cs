namespace Pomodoro.Application.Features.Sessions.Queries;

public static class SessionQueries
{
    public const string GetAllSessions = @"
        SELECT
            ""Id"",
            ""UserId"",
            ""ToDoTaskId"",
            ""DurationMinutes"",
            ""CreatedAt"",
            ""CompletedAt""
        FROM pomodoro_sessions
        WHERE ""UserId"" = @UserId;
        ";
    public const string GetTaskSessions = """
        SELECT
            "Id",
            "UserId" AS UserId,
            "ToDoTaskId" AS ToDoTaskId,
            "DurationMinutes",
            "CreatedAt",
            "CompletedAt"
        FROM pomodoro_sessions
        WHERE "UserId" = @UserId AND "ToDoTaskId" = @TaskId;
        """;
    public const string GetQuickSessions = @"
        SELECT
            ""Id"",
            ""UserId"",
            ""ToDoTaskId"",
            ""DurationMinutes"",
            ""CreatedAt"",
            ""CompletedAt""
        FROM pomodoro_sessions
        WHERE ""UserId"" = @UserId AND ""ToDoTaskId"" IS NULL;
        ";
}