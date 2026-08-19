namespace Pomodoro.Domain.Common.Interfaces;

public interface IHasUpdatedAt
{
    DateTime? UpdatedAt { get; }
}