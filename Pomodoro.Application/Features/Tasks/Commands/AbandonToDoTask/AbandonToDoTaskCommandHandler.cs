using Mediator;
using Pomodoro.Application.Common.Interfaces;

namespace Pomodoro.Application.Features.Tasks.Commands.AbandonToDoTask;

public sealed class AbandonToDoTaskCommandHandler : IRequestHandler<AbandonToDoTaskCommand>
{
    private readonly IApplicationDbContext _context;

    public AbandonToDoTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Unit> Handle(AbandonToDoTaskCommand command, CancellationToken cancellationToken = default)
    {
        var task = await _context.GetTaskByIdAsync(command.TaskId, command.UserId, cancellationToken);
        if (task is null)
            throw new KeyNotFoundException($"Task with ID:{command.TaskId} was not found.");
        task.Abandon();
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}