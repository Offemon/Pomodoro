using FluentValidation;

namespace Pomodoro.Application.Features.Authentication.Commands.LogInUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("A valid email format is required.");
        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}