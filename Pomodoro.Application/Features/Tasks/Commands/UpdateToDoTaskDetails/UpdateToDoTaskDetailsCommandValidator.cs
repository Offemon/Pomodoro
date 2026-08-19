using FluentValidation;

namespace Pomodoro.Application.Features.Tasks.Commands.UpdateToDoTaskDetails;

public sealed class UpdateToDoTaskDetailsCommandValidator : AbstractValidator<UpdateToDoTaskDetailsCommand>
{
    public UpdateToDoTaskDetailsCommandValidator()
    {
        RuleFor(c => c.TaskId).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Title).NotEmpty()
            .WithMessage("Task title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
        RuleFor(c => c.Description).MaximumLength(1000);
        RuleFor(c => c.EstimatedPomodoros).NotEmpty()
            .GreaterThanOrEqualTo(1).WithMessage("Estimated pomodoro session count should be greater than or equal to one (1)");
        RuleFor(c => c.DueDate)
            .Must(date => !date.HasValue || date.Value.Date >= DateTime.UtcNow.Date)
            .WithMessage("Due dae cannot be set in he past.");
    }
}