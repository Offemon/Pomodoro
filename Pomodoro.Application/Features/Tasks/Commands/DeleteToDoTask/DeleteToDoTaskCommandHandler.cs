using Mediator;
using Pomodoro.Application.Common.Interfaces;

namespace Pomodoro.Application.Features.Tasks.Commands.DeleteToDoTask;

public class DeleteToDoTaskCommandHandler : IRequestHandler<DeleteToDoTaskCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteToDoTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Unit> Handle(DeleteToDoTaskCommand request, CancellationToken cancellationToken = default)
    {
        var task = await _context.GetTaskByIdAsync(request.TaskId, request.UserId, cancellationToken);
        if (task is null)
            throw new KeyNotFoundException($"Task with ID '{request.TaskId}' was not found.");
        _context.RemoveEntity(task);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}