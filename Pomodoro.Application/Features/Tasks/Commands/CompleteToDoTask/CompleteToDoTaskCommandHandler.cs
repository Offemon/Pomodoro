using Mediator;
using Pomodoro.Application.Common.Interfaces;
namespace Pomodoro.Application.Features.Tasks.Commands.CompleteToDoTask;

public class CompleteToDoTaskCommandHandler : IRequestHandler<CompleteToDoTaskCommand>
{
    private readonly IApplicationDbContext _context;

    public CompleteToDoTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Unit> Handle(CompleteToDoTaskCommand request, CancellationToken cancellationToken = default)
    {
        var task = await _context.GetTaskByIdAsync(request.TaskId, request.UserId, cancellationToken);
        if (task is null)
            throw new KeyNotFoundException($"Task with ID '{request.TaskId}' was not found.");
        task.Complete();
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}