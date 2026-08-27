using Mediator;

namespace Pomodoro.Application.Features.Tasks.Commands.CompleteToDoTaskWithSession;

public sealed record CompleteToDoTaskWithSessionCommand(
    int DurationMinutes
) : IRequest
{
    public Guid ToDoTaskId { get; set; }
    public Guid UserId { get; set; }

}