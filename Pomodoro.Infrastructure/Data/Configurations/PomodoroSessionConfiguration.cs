using Pomodoro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pomodoro.Infrastructure.Data.Configurations;

public class PomodoroSessionConfiguration : IEntityTypeConfiguration<PomodoroSession>
{
    public void Configure(EntityTypeBuilder<PomodoroSession> builder)
    {
        builder.ToTable("pomodoro_sessions");
        builder.HasKey(s => s.Id);
        builder.HasIndex(s => s.UserId);
        builder.HasIndex(s => s.ToDoTaskId);
        builder.Property(s => s.DurationMinutes)
            .IsRequired();
        builder.Property(s => s.CreatedAt)
            .IsRequired();
        builder.Property(s => s.CompletedAt)
            .IsRequired();
        builder.HasOne<ToDoTask>()
            .WithMany()
            .HasForeignKey(s => s.ToDoTaskId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}