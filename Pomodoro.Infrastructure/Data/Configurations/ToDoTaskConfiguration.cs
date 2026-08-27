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
        builder.Property(t => t.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);
        builder.Property(t => t.CreatedAt)
            .IsRequired();
        builder.Property(t => t.EstimatedPomodoros)
            .IsRequired();
        builder.Property(t => t.CompletedPomodoros)
            .IsRequired();
        builder.Property(t => t.DueDate);
        builder.Property(t => t.UpdatedAt);
        builder.Property(t => t.IsAbandoned)
            .IsRequired()
            .HasDefaultValue(false);
        builder.Property(t => t.IsPriority)
            .IsRequired()
            .HasDefaultValue(false);
        builder.Property(t => t.EnergyLevel)
            .IsRequired()
            .HasDefaultValue(TaskEnergyLevel.Low);
    }
}