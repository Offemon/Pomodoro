using Mediator;
using Pomodoro.Application.Common.Interfaces;


namespace Pomodoro.Application.Features.Tasks.Commands.UpdateToDoTaskDetails;

public class UpdateToDoTaskDetailsCommandHandler : IRequestHandler<UpdateToDoTaskDetailsCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateToDoTaskDetailsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Unit> Handle(UpdateToDoTaskDetailsCommand request, CancellationToken cancellationToken = default)
    {
        var task = await _context.GetTaskByIdAsync(request.TaskId, request.UserId, cancellationToken);
        if (task is null)
            throw new KeyNotFoundException($"Task with ID '{request.TaskId}' was not found.");
        task.UpdateDetails(request.Title, request.Description, request.EstimatedPomodoros, request.DueDate);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}