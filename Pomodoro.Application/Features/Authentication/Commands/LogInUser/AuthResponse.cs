namespace Pomodoro.Application.Features.Authentication.Commands.LogInUser;

public sealed record AuthResponse(
        Guid UserId,
        string Email,
        string Token
    );