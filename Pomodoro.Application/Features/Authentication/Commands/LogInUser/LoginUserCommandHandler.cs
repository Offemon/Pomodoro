using Mediator;
using Pomodoro.Application.Common.Interfaces;

namespace Pomodoro.Application.Features.Authentication.Commands.LogInUser;

public sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand
, AuthResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async ValueTask<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken = default)
    {
        var user = await _context.GetUserByEmailAsync(request.Email, cancellationToken);
        if (user is null)
            throw new InvalidOperationException("Invalid email address or password.");
        var isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
        if (!isPasswordValid)
            throw new InvalidOperationException("Invalid email address or password.");
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthResponse(user.Id, user.Email, token);
    }
}