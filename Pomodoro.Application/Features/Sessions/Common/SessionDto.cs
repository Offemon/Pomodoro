namespace Pomodoro.Application.Features.Sessions.Common;

public record SessionDto(
        Guid SessionId,
        Guid? TaskId,
        int Duration,
        DateTime CompletedAt
    );