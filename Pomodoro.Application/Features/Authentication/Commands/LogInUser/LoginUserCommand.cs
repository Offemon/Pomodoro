using Mediator;

namespace Pomodoro.Application.Features.Authentication.Commands.LogInUser;

public record LoginUserCommand(
        string Email,
        string Password
    ):IRequest<AuthResponse>;