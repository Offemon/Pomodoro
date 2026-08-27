using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Features.Sessions.Commands.LogPomodoroSession;
using Pomodoro.Application.Features.Tasks.Commands.IncrementTaskPomodoro;

namespace Pomodoro.Application.Features.Tasks.Commands.CompleteToDoTaskWithSession;

public sealed class CompleteToDoTaskWithSessionCommandHandler : IRequestHandler<CompleteToDoTaskWithSessionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ISender _mediator;

    public CompleteToDoTaskWithSessionCommandHandler(IApplicationDbContext context, ISender mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async ValueTask<Unit> Handle(CompleteToDoTaskWithSessionCommand command, CancellationToken cancellationToken = default)
    {
        await using var transaction = await _context.BeginTransactionAsync(cancellationToken);
        try
        {
            var task = await _context.GetTaskByIdAsync(command.ToDoTaskId, command.UserId, cancellationToken);
            if (task is null)
                throw new KeyNotFoundException($"Tasks with ID '{command.ToDoTaskId} was not found");
            task.Complete();
            await _context.SaveChangesAsync(cancellationToken);
            await _mediator.Send(new LogPomodoroSessionCommand(command.ToDoTaskId, command.DurationMinutes){VerifiedUserId = command.UserId}, cancellationToken);
            await _mediator.Send(new IncrementTaskPomodoroCommand(command.ToDoTaskId, command.UserId), cancellationToken);
            await transaction.CommitAsync(cancellationToken);

        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        return Unit.Value;
    }
}