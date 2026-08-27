namespace Pomodoro.Domain.Entities;

public sealed class TaskTimelineEvent
{
    public TaskTimelineEvent()
    {
        
    }

    public TaskTimelineEvent(Guid id, Guid taskId, Guid userId, string eventType, DateTime occuredAt, string metaDataJson)
    {
        Id = id;
        TaskId = taskId;
        UserId = userId;
        EventType = eventType;
        OccuredAt = occuredAt;
        MetaDataJson = metaDataJson;
    }

    public Guid Id { get; private set; }
    public Guid TaskId { get; private set; }
    public Guid UserId { get; private set; }
    public string EventType { get; private set; }
    public DateTime OccuredAt { get; private set; }
    
    public string MetaDataJson { get; private set; }
}