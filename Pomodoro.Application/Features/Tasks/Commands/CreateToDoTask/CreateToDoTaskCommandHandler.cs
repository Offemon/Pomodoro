using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Tasks.Commands.CreateToDoTask;

public sealed class CreateToDoTaskCommandHandler : IRequestHandler<CreateToDoTaskCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateToDoTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Guid> Handle(CreateToDoTaskCommand request, CancellationToken cancellationToken = default)
    {
        var userExists = await _context.UserExistsAsync(request.UserId, cancellationToken);
        if (!userExists)
        {
            throw new KeyNotFoundException($"User with ID '{request.UserId}' not found.'");
        }

        var task = new ToDoTask(
            Guid.NewGuid(),
            request.UserId,
            request.Title,
            request.Description,
            request.EstimatedPomodoros,
            request.DueDate,
            request.IsPriority,
            request.EnergyLevel
            );
        _context.AddEntity(task);
        await _context.SaveChangesAsync(cancellationToken);
        return task.Id;
    }
}