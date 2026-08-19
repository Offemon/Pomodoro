using Mediator;
using Pomodoro.Application.Common.Interfaces;
using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Features.Authentication.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    public RegisterUserCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }
    public async ValueTask<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var isEmailUnique = await _context.IsEmailUniqueAsync(request.Email);
        if (!isEmailUnique)
        {
            throw new InvalidOperationException($"A user with the email '{request.Email}' already exists.");
        }

        var temporaryHash = _passwordHasher.HashPassword(request.Password);
        var newUser = new User(
            Guid.NewGuid(),
            request.Email,
            temporaryHash
            );
        _context.AddEntity(newUser);
        await _context.SaveChangesAsync(cancellationToken);
        return newUser.Id;

    }
}