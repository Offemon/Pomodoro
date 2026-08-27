using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Application.Features.Tasks.Commands.IncrementTaskPomodoro;
using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Sessions.Commands.LogPomodoroSession;

public sealed class LogPomodoroSessionCommandHandler : IRequestHandler<LogPomodoroSessionCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ISender _mediator;
    public LogPomodoroSessionCommandHandler(IApplicationDbContext context, ISender mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    public async ValueTask<Guid> Handle(LogPomodoroSessionCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _context.UserExistsAsync(request.VerifiedUserId, cancellationToken);
        if (!userExists)
        {
            throw new KeyNotFoundException($"User with ID '{request.VerifiedUserId}' not found.");
        }

        if (request.ToDoTaskId.HasValue)
        {
            var taskValid = await _context.TaskExistsForUserAsync(request.ToDoTaskId.Value, request.VerifiedUserId, cancellationToken);
            if (!taskValid)
                throw new UnauthorizedAccessException(
                    "The specified Task does not exist or does not belong to this user");
        }

        var session = new PomodoroSession(
                Guid.NewGuid(), 
                request.VerifiedUserId,
                request.ToDoTaskId,
                request.DurationMinutes
            );
        _context.AddEntity(session);
        await _context.SaveChangesAsync(cancellationToken);

        if (request.ToDoTaskId.HasValue)
        {
            await _mediator.Send(
                    new IncrementTaskPomodoroCommand(request.ToDoTaskId.Value, request.VerifiedUserId),
                    cancellationToken
                );
        }
        
        return session.Id;
    }
}