using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pomodoro.Domain.Entities;

namespace Pomodoro.Infrastructure.Data.Configurations;

public sealed class TaskTimelineEventConfiguration : IEntityTypeConfiguration<TaskTimelineEvent>
{
    public void Configure(EntityTypeBuilder<TaskTimelineEvent> builder)
    {
        builder.ToTable("task_timeline_events");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.EventType)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(e => e.OccuredAt)
            .IsRequired();
        builder.Property(e => e.MetaDataJson)
            .HasColumnType("jsonb")
            .IsRequired();
        builder.HasOne<ToDoTask>()
            .WithMany()
            .HasForeignKey(e => e.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(e => new { e.TaskId, e.OccuredAt });
    }
}