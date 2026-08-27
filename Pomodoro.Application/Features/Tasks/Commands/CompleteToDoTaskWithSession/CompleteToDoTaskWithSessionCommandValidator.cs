using FluentValidation;

namespace Pomodoro.Application.Features.Tasks.Commands.CompleteToDoTaskWithSession;

public sealed class CompleteToDoTaskWithSessionCommandValidator : AbstractValidator<CompleteToDoTaskWithSessionCommand>
{
    public CompleteToDoTaskWithSessionCommandValidator()
    {
        RuleFor(t => t.DurationMinutes)
            .InclusiveBetween(1, 25)
            .WithMessage("Duration must be at least one minute and twenty-five minutes at most.");
    }
}