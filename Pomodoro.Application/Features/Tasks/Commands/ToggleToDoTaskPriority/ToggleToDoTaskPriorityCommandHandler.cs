using Mediator;
using Pomodoro.Application.Common.Interfaces;

namespace Pomodoro.Application.Features.Tasks.Commands.ToggleToDoTaskPriority;

public sealed class ToggleToDoTaskPriorityCommandHandler : IRequestHandler<ToggleToDoTaskPriorityCommand>
{
    private readonly IApplicationDbContext _context;

    public ToggleToDoTaskPriorityCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Unit> Handle(ToggleToDoTaskPriorityCommand command,
        CancellationToken cancellationToken = default)
    {
        var task = await _context.GetTaskByIdAsync(command.TaskId, command.UserId, cancellationToken);
        if (task is null)
            throw new KeyNotFoundException($"Task with ID: {command.TaskId} was not found.");
        task.TogglePriority();
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}