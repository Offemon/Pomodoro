using Mediator;

namespace Pomodoro.Application.Features.Authentication.Commands.RegisterUser;

public sealed record RegisterUserCommand(
    string Email,
    string Password
    ) : IRequest<Guid>;