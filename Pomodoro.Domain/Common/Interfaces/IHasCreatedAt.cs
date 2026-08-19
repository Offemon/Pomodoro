namespace Pomodoro.Domain.Common.Interfaces;

public interface IHasCreatedAt
{
    DateTime CreatedAt { get; }
}