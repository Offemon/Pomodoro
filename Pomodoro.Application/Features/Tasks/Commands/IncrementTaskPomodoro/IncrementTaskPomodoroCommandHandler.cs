using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Tasks.Commands.IncrementTaskPomodoro;

public class IncrementTaskPomodoroCommandHandler : IRequestHandler<IncrementTaskPomodoroCommand>
{
    private readonly IApplicationDbContext _context;

    public IncrementTaskPomodoroCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Unit> Handle(IncrementTaskPomodoroCommand request, CancellationToken cancellationToken = default)
    {
        var task = await _context.GetTaskByIdAsync(request.TaskId, request.UserId, cancellationToken);
        if (task is null)
            throw new KeyNotFoundException($"Task with {request.TaskId} was not found.");
        task.IncrementCompletedPomodoros();
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}