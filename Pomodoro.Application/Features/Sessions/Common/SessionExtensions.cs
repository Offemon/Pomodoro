using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Sessions.Common;

public static class SessionExtensions
{
    public static SessionDto ToDto(this PomodoroSession entity)
    {
        return new SessionDto(
                entity.Id,
                entity.DurationMinutes,
                entity.CompletedAt
            );
    }
}