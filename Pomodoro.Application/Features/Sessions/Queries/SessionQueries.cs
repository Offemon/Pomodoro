namespace Pomodoro.Application.Features.Sessions.Queries;

public static class SessionQueries
{
    public const string GetAllSessions = """
        SELECT
            "Id",
            "UserId",
            "ToDoTaskId",
            "DurationMinutes",
            "CreatedAt",
            "CompletedAt"
        FROM pomodoro_sessions
        WHERE "UserId" = @UserId;
        """;
    public const string GetAllSessionsPaged ="""
        SELECT
            "Id",
            "UserId",
            "ToDoTaskId",
            "DurationMinutes",
            "CreatedAt",
            "CompletedAt"
        FROM pomodoro_sessions
        WHERE "UserId" = @UserId
        ORDER BY "CompletedAt" DESC
        LIMIT @Take OFFSET @Skip;

        SELECT COUNT(*)
        FROM pomodoro_sessions
        WHERE "UserId" = @UserId;
        """;
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
    public const string GetTaskSessionsPaged ="""
        SELECT
            "Id",
            "UserId",
            "ToDoTaskId",
            "DurationMinutes",
            "CreatedAt",
            "CompletedAt"
        FROM pomodoro_sessions
        WHERE "UserId" = @UserId AND "ToDoTaskId" = @TaskId
        ORDER BY "CompletedAt" DESC
        LIMIT @Take OFFSET @Skip;

        SELECT COUNT(*)
        FROM pomodoro_sessions
        WHERE "UserId" = @UserId AND "ToDoTaskId" = @TaskId;
        """;
    public const string GetQuickSessions = """
        SELECT
            "Id",
            "UserId",
            "ToDoTaskId",
            "DurationMinutes",
            "CreatedAt",
            "CompletedAt"
        FROM pomodoro_sessions
        WHERE "UserId" = @UserId AND "ToDoTaskId" IS NULL;
        """;

    public const string GetQuickSessionsPaged = """
        SELECT
            "Id",
            "UserId",
            "ToDoTaskId",
            "DurationMinutes",
            "CreatedAt",
            "CompletedAt"
        FROM pomodoro_sessions
        WHERE "UserId" = @UserId AND "ToDoTaskId" IS NULL
        ORDER BY "CompletedAt" DESC
        LIMIT @Take OFFSET @Skip;

        SELECT COUNT(*)
        FROM pomodoro_sessions
        WHERE "UserId" = @UserId AND "ToDoTaskId" IS NULL;

        """;
}