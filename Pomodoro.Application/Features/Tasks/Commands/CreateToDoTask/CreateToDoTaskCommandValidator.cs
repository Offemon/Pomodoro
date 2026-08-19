using FluentValidation;

namespace Pomodoro.Application.Features.Tasks.Commands.CreateToDoTask;

public sealed class CreateToDoTaskCommandValidator : AbstractValidator<CreateToDoTaskCommand>
{
    public CreateToDoTaskCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id is required");
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");
        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");
        RuleFor(x => x.EstimatedPomodoros)
            .GreaterThan(0).WithMessage("Estimated Pomodoros must be greater than 0")
            .LessThanOrEqualTo(20).WithMessage("Estimated Pomodoros must be less than or equal to 20");
        RuleFor(x => x.DueDate)
            .Must(date => !date.HasValue || date.Value.Date >= DateTime.UtcNow.Date)
            .WithMessage("Due date must be in the past");
    }   
}