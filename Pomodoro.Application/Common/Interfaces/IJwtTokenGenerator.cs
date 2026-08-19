using Pomodoro.Domain.Entities;

namespace Pomodoro.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}