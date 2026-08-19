using Pomodoro.Application.Common.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace Pomodoro.Infrastructure.Identity;

public sealed class PasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 11;
    public string HashPassword(string password)
    {
        return BC.HashPassword(password, WorkFactor);
    }
    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BC.Verify(password, hashedPassword);
    }
}