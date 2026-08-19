namespace Pomodoro.Application.Features.Sessions.Common;

public record SessionDto(
        Guid SessionId,
        int Duration,
        DateTime CompletedAt
    );