using Pomodoro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pomodoro.Domain.Enums;

namespace Pomodoro.Infrastructure.Data.Configurations;

public class ToDoTaskConfiguration : IEntityTypeConfiguration<ToDoTask>
{
    public void Configure(EntityTypeBuilder<ToDoTask> builder)
    {
        builder.ToTable("todo_tasks");
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => t.UserId);
        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(t => t.Description)
            .HasMaxLength(1000);
        builder.Property(t => t.CreatedAt)
            .IsRequired();
        builder.Property(t => t.OriginalEstimatedPomodoros)
            .IsRequired();
        builder.Property(t => t.EstimatedPomodoros)
            .IsRequired();
        builder.Property(t => t.CompletedPomodoros)
            .IsRequired();
        builder.Property(t => t.OriginalDueDate);
        builder.HasIndex(t => t.DueDate);
        builder.Property(t => t.DueDate);
        builder.Property(t => t.DueDateDelayCount)
            .IsRequired()
            .HasDefaultValue(0);
        builder.Property(t => t.UpdatedAt);
        builder.HasIndex(t => t.CurrentState);
        builder.Property(t => t.CurrentState)
            .IsRequired()
            .HasDefaultValue(TaskState.Active)
            .HasSentinel((TaskState)0);
        builder.Property(t => t.IsPriority)
            .IsRequired()
            .HasDefaultValue(false);
        builder.Property(t => t.EnergyLevel)
            .IsRequired()
            .HasDefaultValue(TaskEnergyLevel.Medium)
            .HasSentinel((TaskEnergyLevel)0);
        builder.Property(t => t.ModifyCount)
            .IsRequired()
            .HasDefaultValue(0);
    }
}