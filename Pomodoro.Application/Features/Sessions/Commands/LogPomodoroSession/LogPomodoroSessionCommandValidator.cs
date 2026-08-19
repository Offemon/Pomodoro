using FluentValidation;

namespace Pomodoro.Application.Features.Sessions.Commands.LogPomodoroSession;

public class LogPomodoroSessionCommandValidator : AbstractValidator<LogPomodoroSessionCommand>
{
    public LogPomodoroSessionCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id is required");
        RuleFor(x => x.DurationMinutes)
            .NotEmpty().WithMessage("Duration is required")
            .Equal(25).WithMessage("Duration must be greater than 0");       
    }
}