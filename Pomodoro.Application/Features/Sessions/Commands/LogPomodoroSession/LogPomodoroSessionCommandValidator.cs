using FluentValidation;

namespace Pomodoro.Application.Features.Sessions.Commands.LogPomodoroSession;

public class LogPomodoroSessionCommandValidator : AbstractValidator<LogPomodoroSessionCommand>
{
    public LogPomodoroSessionCommandValidator()
    {
        RuleFor(x => x.DurationMinutes)
            .NotEmpty().WithMessage("Duration is required")
            .GreaterThan(0).WithMessage("Duration must be greater than 0")
            .LessThanOrEqualTo(25).WithMessage("Duration must be 1 to 25 minutes.");
    }
}